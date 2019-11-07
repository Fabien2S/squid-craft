using System;
using System.IO;
using SquidCraft.API.Assets;
using SquidCraft.API.Utils;

namespace SquidCraft.Assets.Loaders
{
    public class InputStreamLoader : AssetLoader<Stream>
    {
        public override Stream Load(Identifier name, Uri uri)
        {
            return File.OpenRead(uri.LocalPath);
        }
    }
}