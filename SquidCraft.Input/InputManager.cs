using System.Collections.Generic;
using OpenTK.Input;
using SquidCraft.API.Utils;
using SquidCraft.Input.Bindings;

namespace SquidCraft.Input
{
    public class InputManager
    {
        private readonly Dictionary<Identifier, IBinding> _mapping = new Dictionary<Identifier, IBinding>
        {
            {
                Minecraft.Inputs.DebugToggleWireFrame,
                BindingChain.Of(
                    new KeyboardBinding(Key.F3),
                    new KeyboardBinding(Key.Z)
                )
            },
            {
                Minecraft.Inputs.Attack,
                new MouseBinding(MouseButton.Left)
            },
            {
                Minecraft.Inputs.Use,
                new MouseBinding(MouseButton.Right)
            },
            {
                Minecraft.Inputs.LookX,
                new MouseAxisBinding(MouseAxisBinding.Axis.X)
            },
            {
                Minecraft.Inputs.LookY,
                new MouseAxisBinding(MouseAxisBinding.Axis.Y)
            },
            {
                Minecraft.Inputs.Forward,
                new KeyboardBinding(Key.W)
            },
            {
                Minecraft.Inputs.Left,
                new KeyboardBinding(Key.A)
            },
            {
                Minecraft.Inputs.Back,
                new KeyboardBinding(Key.S)
            },
            {
                Minecraft.Inputs.Right,
                new KeyboardBinding(Key.D)
            },
            {
                Minecraft.Inputs.Jump,
                new KeyboardBinding(Key.Space)
            },
            {
                Minecraft.Inputs.Sneak,
                new KeyboardBinding(Key.ShiftLeft)
            },
        };

        public void Register(Identifier name, IBinding binding)
        {
            _mapping[name] = binding;
        }

        public IBinding this[Identifier name] => _mapping[name];
    }
}