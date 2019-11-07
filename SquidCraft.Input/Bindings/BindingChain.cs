using System.Linq;

namespace SquidCraft.Input.Bindings
{
    public class BindingChain : IBinding
    {
        public bool Pressed => _bindings.All(binding => binding.Pressed);
        public float Value => _bindings.Aggregate(1f, (current, b) => current * b.Value);

        private readonly IBinding[] _bindings;

        public BindingChain(IBinding[] bindings)
        {
            _bindings = bindings;
        }

        public static IBinding Of(params IBinding[] bindings)
        {
            return new BindingChain(bindings);
        }
    }
}