using OpenTK.Input;

namespace SquidCraft.Input.Bindings
{
    public class JoystickBinding : IBinding
    {
        public bool Pressed
        {
            get
            {
                var state = Joystick.GetState(_joystick);
                return state.IsButtonDown(_button);
            }
        }

        public float Value => Pressed ? 1 : 0;

        private readonly int _joystick;
        private readonly int _button;

        public JoystickBinding(int joystick, int button)
        {
            _button = button;
            _joystick = joystick;
        }
    }
}