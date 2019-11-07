using SquidCraft.API.Utils;

namespace SquidCraft.API.Assets
{
    public interface IAssetManager
    {
        void LoadRegistry();
        bool TryLoad<T>(Identifier name, out T asset);
        T Load<T>(Identifier name);
    }
}