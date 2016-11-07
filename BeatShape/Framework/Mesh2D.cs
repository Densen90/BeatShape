using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    class Mesh2D
    {
        #region object data
        public Vector3[] Vertices { get; set; }

        public Vector3[] Colors { get; set; }

        public Matrix4[] ModelViews { get; set; }

        Shader shader;
        Shader Shader
        {
            get { return shader; }
            set { shader = value; }
        }
        #endregion

        #region attributes
        private int attribute_vcol;
        private int attribute_vpos;
        private int uniform_mview;
        #endregion

        #region vertex buffer objects
        private int vbo_position;
        private int vbo_color;
        private int vbo_mview;
        #endregion

        public Mesh2D()
        {
            Vector3[] vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};


            Vector3[] coldata = new Vector3[] { new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)};


            Matrix4[] mviewdata = new Matrix4[]{
                Matrix4.Identity
            };
            init(vertdata, coldata, mviewdata);
        }

        public Mesh2D(Vector3[] vertices, Vector3[] colors, Matrix4[] modelviews)
        {
            init(vertices, colors, modelviews);
        }

        private void init(Vector3[] vertices, Vector3[] colors, Matrix4[] modelviews)
        {
            Vertices = vertices;
            Colors = colors;
            ModelViews = modelviews;

            Shader = ShaderLoader.FromFile("Shader\\vertex.glsl", "Shader\\fragment.glsl");

            attribute_vpos = shader.GetAttributeLocation("vPosition");
            attribute_vcol = shader.GetAttributeLocation("vColor");
            uniform_mview = shader.GetUniformLocation("modelview");

            if (attribute_vpos == -1 || attribute_vcol == -1 || uniform_mview == -1)
            {
                Console.WriteLine("Error binding attributes");
            }

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mview);
        }

        public void Prepare()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * Vector3.SizeInBytes), Vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Colors.Length * Vector3.SizeInBytes), Colors, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.UniformMatrix4(uniform_mview, false, ref ModelViews[0]);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Render()
        {
            Shader.Begin();

            //Enable attribute
            GL.EnableVertexAttribArray(attribute_vpos);
            GL.EnableVertexAttribArray(attribute_vcol);

            //Draw triangle
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            //Clean
            GL.DisableVertexAttribArray(attribute_vpos);
            GL.DisableVertexAttribArray(attribute_vcol);
        }
    }
}
