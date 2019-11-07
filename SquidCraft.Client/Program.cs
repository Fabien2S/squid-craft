namespace SquidCraft.Client
{
    internal static class Program
    {
        private static void Main()
        {
            var opt = new ClientOptions
            {
                AssetDir = @"D:\Software\Minecraft\Minecraft_Data\assets",
                AssetIndex = "1.14"
            };

            using var client = new MinecraftClient(opt);

            client.Initialize();
            client.Run();
        }
    }
}