using SquidCraft.API.Utils;

namespace SquidCraft.Adapters
{
    public interface IGameAdapter
    {
        GameVersion Version { get; }

        /*NetworkState NetworkState { get; }

        IBlockRegistry BlockRegistry { get; }
        IEntityRegistry EntityRegistry { get; }

        IClientConnection CreateConnection(NetworkClient networkClient);*/
    }
}