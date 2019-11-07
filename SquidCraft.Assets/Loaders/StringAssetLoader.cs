using System;
using System.IO;
using System.Text;
using SquidCraft.API.Assets;
using SquidCraft.API.Utils;

namespace SquidCraft.Assets.Loaders
{
    public class StringAssetLoader : AssetLoader<string>
    {
        public override string Load(Identifier name, Uri uri)
        {
            return File.ReadAllText(uri.LocalPath, Encoding.UTF8);
        }
    }
}