namespace SquidCraft.Rendering.Shaders
{
    public static class SimpleShader
    {
        public static readonly Shader Instance = Shader.Compile(VertexShader, FragmentShader);

        private const string VertexShader = @"#version 330 core
in vec3 in_position;
in vec3 in_color;
in vec3 in_normal;
in vec3 in_uv;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

out vec3 color;
out vec3 normal;
out vec2 uv;

void main()
{
    gl_Position = vec4(in_position, 1.0) * model * view * projection;

    color = in_color;
    normal = in_normal;
    uv = in_uv.xy;
}";

        private const string FragmentShader = @"#version 330 core
out vec4 out_color;

in vec3 color;
in vec3 normal;
in vec2 uv;

uniform sampler2D mainTexture;

void main()
{
    out_color = texture(mainTexture, uv) * vec4(color, 1.0);
}";
    }
}