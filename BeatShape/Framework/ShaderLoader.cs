using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace BeatShape.Framework
{
    static class ShaderLoader
    {
        /// <summary>
		/// Compiles and links vertex and fragment shaders from strings
		/// </summary>
		/// <param name="vertexSource">The source code of the vertex shader</param>
		/// <param name="fragmentSource">The source code of the fragmenr shader</param>
		/// <returns>a new instance of <see cref="Shader"/> class</returns>
        public static Shader FromSource(string vertexSource, string fragmentSource)
        {
            Shader shader = new Shader();

            try
            {
                shader.Compile(vertexSource, ShaderType.VertexShader);
                shader.Compile(fragmentSource, ShaderType.FragmentShader);
                shader.Link();
            }
            catch (Exception e)
            {
                shader.Dispose();
                throw e;
            }

            return shader;
        }

        /// <summary>
        /// Compiles and links vertex and fragment shaders from files
        /// </summary>
        /// <param name="vertexSource">The filename of the vertex shader</param>
        /// <param name="fragmentSource">The filename of the fragmenr shader</param>
        /// <returns>a new instance of <see cref="Shader"/> class</returns>
        public static Shader FromFile(string vertexFilename, string fragmentFilename)
        {
            string vertexString = ShaderStringFromFile(vertexFilename);
            string fragmentString = ShaderStringFromFile(fragmentFilename);

            return FromSource(vertexString, fragmentString);
        }

        /// <summary>
        /// Reads the contents of a file into a string
        /// </summary>
        /// <param name="shaderFile">path to the shader file</param>
        /// <returns>string with contents of shaderFile</returns>
        public static string ShaderStringFromFile(string filename)
        {
            string sShader = null;
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException("Could not find shader file '" + filename + "'");
            }
            sShader = File.ReadAllText(filename);

            return sShader;
        }
    }
}
