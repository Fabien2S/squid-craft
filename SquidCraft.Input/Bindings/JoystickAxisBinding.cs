using OpenTK.Input;

namespace SquidCraft.Input.Bindings
{
    public class JoystickAxisBinding : IBinding
    {
        public bool Pressed => System.Math.Abs(Value) > float.Epsilon;

        public float Value
        {
            get
            {
                var state = Joystick.GetState(_joystick);
                return state.GetAxis(_axis);
            }
        }

        private readonly int _joystick;
        private readonly int _axis;

        public JoystickAxisBinding(int joystick, int axis)
        {
            _axis = axis;
            _joystick = joystick;
        }
    }
}