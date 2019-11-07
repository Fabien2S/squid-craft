using System;
using System.Drawing;
using System.Globalization;
using NLog;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using SquidCraft.API.Assets;
using SquidCraft.API.Audio;
using SquidCraft.API.I18N;
using SquidCraft.API.Math;
using SquidCraft.API.Rendering;
using SquidCraft.Assets;
using SquidCraft.Audio;
using SquidCraft.Client.Controller;
using SquidCraft.I18N;
using SquidCraft.Input;
using SquidCraft.Rendering;
using MathHelper = SquidCraft.Math.MathHelper;

namespace SquidCraft.Client
{
    public class MinecraftClient : IDisposable
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        
        private readonly GameWindow _window;
        
        private readonly IAssetManager _assetManager;
        private readonly IAudioManager _audioManager;
        private readonly IRenderEngine _renderEngine;
        private readonly II18NManager _i18NManager;
        private readonly InputManager _inputManager;

        private readonly ICamera _camera;
        
        private PlayerController _playerController;

        public MinecraftClient(ClientOptions options, int width = 960, int height = 540)
        {
            _window = new GameWindow(
                width, height,
                GraphicsMode.Default,
                "SquidCraft",
                GameWindowFlags.Default,
                DisplayDevice.Default, 
                3, 3,
                GraphicsContextFlags.Debug
            );
            
            _assetManager = new AssetManager(options.AssetDir, options.AssetIndex);
            _audioManager = new AudioManager();
            _renderEngine = new RenderEngine(_assetManager);
            _i18NManager = new I18NManager(_assetManager);
            _inputManager = new InputManager();
            
            _camera = new Camera(Vector3.Zero, Rotation.Zero);
            _playerController = new FlyAroundController(
                _camera,
                _inputManager
            );

            _window.CursorVisible = false;
            _window.CursorGrabbed = true;
        }

        public void Initialize()
        {
            _assetManager.LoadRegistry();
            _i18NManager.LoadLanguage(CultureInfo.CurrentCulture);
            
            _audioManager.Listener = _camera;
            
            // registering window event
            _window.Load += OnLoad;
            _window.Closed += OnClosed;
            _window.Resize += OnResize;

            _window.KeyDown += OnKeyDown;
            _window.KeyUp += OnKeyUp;
            
            _window.UpdateFrame += OnUpdateFrame;
            _window.RenderFrame += OnRenderFrame;
            
            // apply icon
            var iconIdentifier = Minecraft.CreateIdentifier("icons/icon_32x32.png");
            var icon = _assetManager.Load<Bitmap>(iconIdentifier);
            var iconHandle = icon.GetHicon();
            _window.Icon = Icon.FromHandle(iconHandle);
        }

        public void Run()
        {
            _window.Run(60);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _renderEngine.Init();
            _camera.FieldOfView.BaseValue = 85 * MathHelper.DegreesToRadians;
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Dispose();
        }

        private void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(_window.ClientSize);
            _camera.AspectRatio = (float) _window.Width / _window.Height;
        }

        private void OnKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            var keyboard = e.Keyboard;
            if(keyboard.IsKeyUp(Key.F3))
                return;
            
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.Key)
            {
                case Key.Z:
                    _renderEngine.Wireframe = !_renderEngine.Wireframe;
                    break;
            }
        }

        private void OnKeyUp(object sender, KeyboardKeyEventArgs e)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (e.Key)
            {
                case Key.F11:
                {
                    var state = _window.WindowState;
                    _window.WindowState = state == WindowState.Fullscreen ? WindowState.Normal : WindowState.Fullscreen;
                    return;
                }
                case Key.Escape:
                    _window.Exit();
                    return;
            }
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            var deltaTime = (float) e.Time;
            
            if (_window.Focused)
                _playerController.Update(deltaTime);

            _audioManager.Update(deltaTime);
            _renderEngine.Update(deltaTime);
        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            _window.Title = $"FPS: {1f / e.Time:0}";

            _renderEngine.Render(_camera, _window.Context, e.Time);

            _window.SwapBuffers();
        }

        public void Dispose()
        {
            _window.Dispose();
            _audioManager.Dispose();
            _renderEngine.Dispose();
        }
    }
}