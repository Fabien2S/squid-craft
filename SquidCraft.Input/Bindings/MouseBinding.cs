using OpenTK.Input;

namespace SquidCraft.Input.Bindings
{
    public class MouseBinding : IBinding
    {
        public bool Pressed
        {
            get
            {
                var state = Mouse.GetState();
                return state[_button];
            }
        }

        public float Value => Pressed ? 1 : 0;

        private readonly MouseButton _button;

        public MouseBinding(MouseButton button)
        {
            _button = button;
        }
    }
}