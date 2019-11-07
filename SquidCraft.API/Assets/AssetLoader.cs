using System;
using SquidCraft.API.Utils;

namespace SquidCraft.API.Assets
{
    public static class AssetLoader
    {
        public static AssetLoader<T> GetLoader<T>() => AssetLoader<T>.Instance;

        public static void Register<TLoader, TValue>() where TLoader : AssetLoader<TValue>, new()
        {
            var loader = new TLoader();
            if (AssetLoader<TValue>.Instance != loader)
                throw new InvalidOperationException("Unable to register the loader");
        }
    }

    public abstract class AssetLoader<T>
    {
        public static AssetLoader<T> Instance { get; private set; }

        protected AssetLoader()
        {
            if (Instance != null)
                throw new InvalidOperationException("More than one loader has been defined for type " + typeof(T));
            Instance = this;
        }

        public abstract T Load(Identifier name, Uri uri);

        public override string ToString()
        {
            return typeof(T).Name;
        }
    }
}