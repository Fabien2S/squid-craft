using System;
using OpenTK.Input;

namespace SquidCraft.Input.Bindings
{
    public delegate int MouseAxisSupplier(MouseState state);

    public class MouseAxisBinding : IBinding
    {
        public bool Pressed => Value > 0;

        public float Value
        {
            get
            {
                var mouseState = Mouse.GetState();
                var value = _axisSupplier(mouseState);
                var delta = _previousValue - value;
                _previousValue = value;
                return delta;
            }
        }

        private readonly MouseAxisSupplier _axisSupplier;
        private int _previousValue;

        public MouseAxisBinding(Axis axis)
        {
            _axisSupplier = axis switch
            {
                Axis.X => (MouseAxisSupplier) (state => state.X),
                Axis.Y => (MouseAxisSupplier) (state => state.Y),
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }

        public enum Axis
        {
            X,
            Y
        }
    }
}