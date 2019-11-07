using System;
using System.Drawing;
using System.IO;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SquidCraft.API.Assets;
using SquidCraft.API.Rendering;
using SquidCraft.API.Utils;
using SquidCraft.Rendering.Models;
using SquidCraft.Rendering.Models.Primitives;
using SquidCraft.Rendering.Shaders;
using SquidCraft.Rendering.Textures;

namespace SquidCraft.Rendering.Renderers.Sky
{
    public class SkyBoxRenderer : IRenderable, IDisposable
    {
        private readonly Identifier _name;
        private readonly Mesh _mesh;
        
        private CubeMap _texture;

        public SkyBoxRenderer(Identifier name)
        {
            _name = name;
            _mesh = Cube.Build(true);
            _mesh.Prepare(SkyboxShader.Instance);
        }

        public void Load(IAssetManager assetManager)
        {
            _texture = new CubeMap();
            _texture.Bind();
            
            _texture.Param(TextureParameterName.TextureWrapS, TextureWrapMode.ClampToEdge);
            _texture.Param(TextureParameterName.TextureWrapT, TextureWrapMode.ClampToEdge);
            _texture.Param(TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            _texture.Param(TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);

            for (var i = 0; i < 6; i++)
            {
                var textureName = new Identifier(_name.Namespace, _name.Key + '_' + i + ".png");
                if (!assetManager.TryLoad<Bitmap>(textureName, out var image))
                    throw new FileNotFoundException("Texture not found");
                
                _texture.SetIndex((CubeMap.TextureIndex) i);
                _texture.Data(image);
            }
            
            
        }

        public void Render(ICamera camera, IGraphicsContext context, double deltaTime)
        {
            _texture.Bind();
            _mesh.Render();
        }

        public void Dispose()
        {
            _mesh.Dispose();
            _texture.Dispose();
        }
    }
}