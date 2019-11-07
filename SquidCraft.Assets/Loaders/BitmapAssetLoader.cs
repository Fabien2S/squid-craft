using System;
using System.Drawing;
using SquidCraft.API.Assets;
using SquidCraft.API.Utils;

namespace SquidCraft.Assets.Loaders
{
    public class BitmapAssetLoader : AssetLoader<Bitmap>
    {
        public override Bitmap Load(Identifier name, Uri uri)
        {
            return new Bitmap(uri.LocalPath);
        }
    }
}