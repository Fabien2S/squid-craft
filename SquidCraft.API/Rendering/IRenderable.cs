using OpenTK.Graphics;

namespace SquidCraft.API.Rendering
{
    public interface IRenderable
    {
        void Render(ICamera camera, IGraphicsContext context, double deltaTime);
    }
}