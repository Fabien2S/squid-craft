namespace SquidCraft.Input.Bindings
{
    public interface IBinding
    {
        bool Pressed { get; }
        float Value { get; }
    }
}