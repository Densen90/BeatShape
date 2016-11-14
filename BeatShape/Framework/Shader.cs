using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    class Shader : IDisposable
    {
        private int programID = 0;

        public bool IsLinked { get; private set; }
        public string LastLog { get; private set; }

        /// <summary>
		/// Constructor, initialize a new instance of the <see cref="Shader"/> class
		/// </summary>
        public Shader()
        {
            programID = GL.CreateProgram();
        }

        /// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources. Called from <see cref="IDisposable"/> class
		/// </summary>
        public void Dispose()
        {
            if(programID!=0)
            {
                GL.DeleteProgram(programID);
            }
        }

        /// <summary>
		/// Compile the given SourceCode against the given type. 
		/// </summary>
        /// <param name="shaderCode">Source code</param>
        /// <param name="type">Shadertype</param>
        public void Compile(string shaderCode, ShaderType type)
        {
            IsLinked = false;
            int shaderObject = GL.CreateShader(type);

            if (shaderObject == 0) throw new ShaderException(type.ToString(), "Could not create shader object", string.Empty, shaderCode);

            //Compile shader
            GL.ShaderSource(shaderObject, shaderCode);
            GL.CompileShader(shaderObject);

            //Check status of compilation
            int status;
            GL.GetShader(shaderObject, ShaderParameter.CompileStatus, out status);
            LastLog = GL.GetShaderInfoLog(shaderObject);

            if(status!=1) throw new ShaderException(type.ToString(), "Error compiling shader", LastLog, shaderCode);

            //attach shader to program
            GL.AttachShader(programID, shaderObject);
        }

        /// <summary>
		/// Begins this shader use.
		/// </summary>
		public void Begin()
        {
            GL.UseProgram(programID);
        }

        /// <summary>
        /// Ends this shader use.
        /// </summary>
        public void End()
        {
            GL.UseProgram(0);
        }

        /// <summary>
        /// Get Location of given attribute
        /// </summary>
        /// <param name="name">name of attribute</param>
        public int GetAttributeLocation(string name)
        {
            return GL.GetAttribLocation(programID, name);
        }

        /// <summary>
        /// Get Location of given uniform
        /// </summary>
        /// <param name="name">name of uniform</param>
        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(programID, name);
        }

        /// <summary>
        /// Link the program of the shader
        /// </summary>
        public void Link()
        {
            try
            {
                GL.LinkProgram(programID);
            }
            catch (Exception)
            {
                throw new ShaderException("Link", "Unknown error!", string.Empty, string.Empty);
            }
            int status_code;
            GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out status_code);
            if (status_code != 1) throw new ShaderException("Link", "Error linking shader", GL.GetProgramInfoLog(programID), string.Empty);

            IsLinked = true;
        }
    }

    public class ShaderException : Exception
    {
        public string Type { get; private set; }
        public string ShaderCode { get; private set; }
        public string Log { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShaderException"/> class.
        /// </summary>
        /// <param name="msg">The error msg.</param>
        public ShaderException(string type, string msg, string log, string shaderCode) : base(msg)
        {
            Type = type;
            Log = log;
            ShaderCode = shaderCode;
        }
    }
}
