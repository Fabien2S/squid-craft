namespace SquidCraft.Rendering.Shaders
{
    public static class SkyboxShader
    {
        public static readonly Shader Instance = Shader.Compile(VertexShader, FragmentShader);

        private const string VertexShader = @"#version 330 core
in vec3 in_position;
in vec3 in_color;
in vec3 in_uv;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;
  
out vec3 color;
out vec3 uv;

void main()
{
    gl_Position = vec4(in_position, 1.0) * model * view * projection;

    color = in_color;
    uv = in_uv;
}";

        private const string FragmentShader = @"#version 330 core
out vec4 out_color;

in vec3 color;
in vec3 uv;
  
uniform samplerCube skyTexture;

void main()
{
    out_color = texture(skyTexture, uv) * vec4(color, 1.0);
}";
    }
}