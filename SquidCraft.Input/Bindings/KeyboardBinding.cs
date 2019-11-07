using System.Linq;
using OpenTK.Input;

namespace SquidCraft.Input.Bindings
{
    public class KeyboardBinding : IBinding
    {
        public bool Pressed
        {
            get
            {
                var state = Keyboard.GetState();

                var pressed = state[_key];
                return pressed;
                /*if (!pressed)
                    return false;
                
                if (_modifiers == 0)
                    return true;

                if (_modifiers.HasFlag(KeyModifiers.Alt) && NonePressed(state, Key.AltLeft, Key.AltRight))
                    return false;
                if (_modifiers.HasFlag(KeyModifiers.Control) && NonePressed(state, Key.ControlLeft, Key.ControlRight))
                    return false;
                if (_modifiers.HasFlag(KeyModifiers.Shift) && NonePressed(state, Key.ShiftLeft, Key.ShiftRight))
                    return false;
                if (_modifiers.HasFlag(KeyModifiers.Command) && NonePressed(state, Key.Command))
                    return false;
                    
                return true;*/
            }
        }

        public float Value => Pressed ? 1 : 0;

        private readonly Key _key;
        private readonly KeyModifiers _modifiers;

        public KeyboardBinding(Key key, KeyModifiers modifiers = 0)
        {
            _key = key;
            _modifiers = modifiers;
        }

        private static bool NonePressed(KeyboardState state, params Key[] keys)
        {
            return keys.All(key => !state.IsKeyDown(key));
        }
    }
}