using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using NLog;
using SquidCraft.API.Assets;
using SquidCraft.API.Utils;
using SquidCraft.Assets.Exceptions;
using SquidCraft.Assets.Loaders;
using SquidCraft.Json;

namespace SquidCraft.Assets
{
    public class AssetManager : IAssetManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly string _assetPath;
        private readonly string _assetIndex;

        private readonly HashSet<string> _additionalPaths = new HashSet<string>();

        private AssetRegistry _registry;

        public AssetManager(string assetPath, string assetIndex)
        {
            _assetPath = assetPath;
            _assetIndex = assetIndex;

            AssetLoader.Register<InputStreamLoader, Stream>();
            AssetLoader.Register<StringAssetLoader, string>();
            AssetLoader.Register<BitmapAssetLoader, Bitmap>();
        }

        public void LoadRegistry()
        {
            var registryPath = Path.Combine(_assetPath, "indexes", _assetIndex + ".json");
            
            Logger.Info("Loading asset registry from \"{0}\"", registryPath);
            var text = File.ReadAllText(registryPath, Encoding.UTF8);
            _registry = JsonSerializer.Deserialize<AssetRegistry>(text, DefaultJsonSettings.Options);
        }
        
        private Uri ResolveAssetIdentifier(Identifier identifier, bool includeAdditionalPaths = true)
        {
            var assetPath = identifier.Namespace + '/' + identifier.Key;

            if (includeAdditionalPaths)
            {
                // check additional assets (eg. Resource Packs)
                var additionalAssetPath = _additionalPaths.Select(path => path + "/assets/" + assetPath).FirstOrDefault(File.Exists);
                if (additionalAssetPath != null)
                    return new Uri(additionalAssetPath);
            }

            // check global assets
            var assetObjects = _registry.Objects;
            if (assetObjects.ContainsKey(assetPath))
            {
                var assetObject = assetObjects[assetPath];
                var hash = assetObject.Hash;
                var shortHash = hash.Substring(0, 2);
                var globalAssetPath = Path.Combine(_assetPath, "objects", shortHash, hash);
                if (File.Exists(globalAssetPath))
                    return new Uri(globalAssetPath);
            }

            // check per-version assets
            var gameAssetPath = Path.Combine(_assetPath, "versions", _assetIndex, "assets", assetPath);
            return File.Exists(gameAssetPath) ? new Uri(gameAssetPath) : null;
        }

        public T Load<T>(Identifier name)
        {
            if (TryLoad<T>(name, out var asset))
                return asset;

            throw new AssetNotFoundException("Asset \"" + name + "\" not found");
        }
        

        public bool TryLoad<T>(Identifier name, out T asset)
        {
            var uri = ResolveAssetIdentifier(name);
            if (uri == null)
            {
                asset = default;
                return false;
            }
            
            Logger.Debug("Loading asset \"{0}\"", name);
            var loader = AssetLoader.GetLoader<T>();
            if (loader == null)
            {
                asset = default;
                return false;
            }

            asset = loader.Load(name, uri);
            return true;
        }
    }
}