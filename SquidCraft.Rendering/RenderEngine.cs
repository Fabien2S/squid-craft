using System;
using System.Runtime.InteropServices;
using NLog;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using SquidCraft.API.Assets;
using SquidCraft.API.Rendering;
using SquidCraft.Rendering.Renderers.Sky;
using SquidCraft.Rendering.Shaders;
using MathHelper = SquidCraft.Math.MathHelper;

namespace SquidCraft.Rendering
{
    public class RenderEngine : IRenderEngine
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public bool Wireframe
        {
            get => _wireframe;
            set
            {
                GL.PolygonMode(MaterialFace.FrontAndBack, value ? PolygonMode.Line : PolygonMode.Fill);
                _wireframe = value;
            }
        }

        private readonly IAssetManager _assetManager;

        private SkyBoxRenderer _skyBoxRenderer;

        private bool _wireframe;
        
        public RenderEngine(IAssetManager assetManager)
        {
            _assetManager = assetManager;
        }

        public void Init()
        {
            GL.Enable(EnableCap.DebugOutputSynchronous);
            GL.DebugMessageCallback(
                (src, type, id, severity, length, message, param) =>
                {
                    var msg = Marshal.PtrToStringAnsi(message, length);
                    Logger.Log(LogLevel.Debug, "[OpenGL/{0}] {1}", src, msg);
                },
                IntPtr.Zero
            );

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            
            // enable cull face
            GL.Enable(EnableCap.CullFace);
            GL.CullFace(CullFaceMode.Front);

            Wireframe = false;
            
            Fallback.Init();

            var skyBoxTexture = Minecraft.CreateIdentifier("textures/gui/title/background/panorama");
            _skyBoxRenderer = new SkyBoxRenderer(skyBoxTexture);
            _skyBoxRenderer.Load(_assetManager);
        }

        public void Update(float deltaTime)
        {
            _time = (_time + deltaTime) % 360;
        }

        private float _time;
        
        public void Render(ICamera camera, IGraphicsContext context, double deltaTime)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.Enable(EnableCap.DepthTest);

            var model = Matrix4.Identity
                        * Matrix4.CreateRotationY(_time * MathHelper.DegreesToRadians);
                        //* Matrix4.CreateRotationX(25 * MathHelper.DegreesToRadians);
            
            var shader = SkyboxShader.Instance;
            shader.Use();
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", camera.ViewMatrix);
            shader.SetMatrix4("projection", camera.ProjectionMatrix);
            
            _skyBoxRenderer.Render(camera, context, deltaTime);
        }

        public void Dispose()
        {
            _skyBoxRenderer.Dispose();
        }
    }
}