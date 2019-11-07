using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SquidCraft.Rendering.Shaders.Exceptions;

namespace SquidCraft.Rendering.Shaders
{
    public class Shader : IDisposable
    {
        private readonly int _handle;
        private readonly Dictionary<string, int> _uniformLocations;

        private Shader(in int handle)
        {
            _handle = handle;
            
            GL.GetProgram(_handle, GetProgramParameterName.ActiveUniforms, out var count);
            _uniformLocations = new Dictionary<string, int>(count);
            for (var i = 0; i < count; i++)
            {
                var name = GL.GetActiveUniform(_handle, i, out _, out _);
                _uniformLocations[name] = GL.GetUniformLocation(_handle, name);
            }
        }

        public void Use()
        {
            GL.UseProgram(_handle);
        }

        public int Param(string param)
        {
            return GL.GetAttribLocation(_handle, param);
        }

        public void SetBool(string param, bool value)
        {
            var id = _uniformLocations[param];
            GL.Uniform1(id, value ? 1 : 0);
        }

        public void SetInt(string param, int value)
        {
            var id = _uniformLocations[param];
            GL.Uniform1(id, value);
        }

        public void SetFloat(string param, float value)
        {
            var id = _uniformLocations[param];
            GL.Uniform1(id, value);
        }

        public void SetMatrix4(string param, Matrix4 value)
        {
            var id = _uniformLocations[param];
            GL.UniformMatrix4(id, true, ref value);
        }

        public void Dispose()
        {
            _uniformLocations.Clear();
            
            GL.DeleteProgram(_handle);
            GC.SuppressFinalize(this);
        }

        public static void Unbind()
        {
            GL.UseProgram(0);
        }

        private static int CompileShader(ShaderType type, string source)
        {
            var shader = GL.CreateShader(type);

            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            var shaderLog = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(shaderLog))
                throw new ShaderCompileException(shaderLog);

            return shader;
        }

        public static Shader Compile(string vertexShaderSrc, string fragmentShaderSrc)
        {
            var shaders = new[]
            {
                CompileShader(ShaderType.VertexShader, vertexShaderSrc),
                CompileShader(ShaderType.FragmentShader, fragmentShaderSrc)
            };

            var program = GL.CreateProgram();

            foreach (var shader in shaders)
            {
                GL.AttachShader(program, shader);
            }

            GL.LinkProgram(program);

            var programLog = GL.GetProgramInfoLog(program);
            if (!string.IsNullOrEmpty(programLog))
                throw new ShaderCompileException(programLog);

            // clean-up
            foreach (var shader in shaders)
            {
                //GL.DetachShader(program, shader);
                GL.DeleteShader(shader);
            }

            return new Shader(program);
        }
    }
}