using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using NLog;
using SquidCraft.API.Assets;
using SquidCraft.API.I18N;
using SquidCraft.API.Utils;

namespace SquidCraft.I18N
{
    public class I18NManager : II18NManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public string Locale { get; private set; } = "en_US";

        private readonly IAssetManager _assetManager;
        private readonly Dictionary<int, string> _registry = new Dictionary<int, string>();

        public I18NManager(IAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public void LoadLanguage(CultureInfo cultureInfo)
        {
            var langAssetPath = CultureInfoToAssetPath(cultureInfo);
            Logger.Info("Loading language from culture info {0} at \"{1}\"", cultureInfo, langAssetPath);
            
            var langContent = _assetManager.Load<string>(langAssetPath);

            
            var parentCulture = cultureInfo.Parent;
            Locale = cultureInfo.TwoLetterISOLanguageName + '_' + parentCulture.TwoLetterISOLanguageName;

            _registry.Clear();
            
            var document = JsonDocument.Parse(langContent);
            var rootElement = document.RootElement;

            var enumerator = rootElement.EnumerateObject();
            foreach (var property in enumerator)
            {
                var key = property.Name;
                var keyHash = key.GetHashCode();
                
                var jsonElement = property.Value;
                var value = jsonElement.GetString();
                
                _registry[keyHash] = value;
            }

            var entryCount = _registry.Count;
            Logger.Info("Language file successfully loaded ({0} entries found}", entryCount);
        }

        public string this[int key] => _registry[key];

        private static Identifier CultureInfoToAssetPath(CultureInfo cultureInfo)
        {
            var parentCulture = cultureInfo.Parent;
            var langName = cultureInfo.TwoLetterISOLanguageName + '_' + parentCulture.TwoLetterISOLanguageName;
            return Minecraft.CreateIdentifier("lang/" + langName + ".json");
        }
    }
}