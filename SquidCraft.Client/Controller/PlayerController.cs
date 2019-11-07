using SquidCraft.API.Utils;
using SquidCraft.Utils;

namespace SquidCraft.Client.Controller
{
    public abstract class PlayerController : IUpdatable
    {
        public abstract void Update(float deltaTime);
    }
}