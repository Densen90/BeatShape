using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace BeatShape.Framework
{
    class Mesh2D
    {
        #region object data
        public Vector3[] Vertices { get; set; }
        public Vector3[] Colors { get; set; }
        public Matrix4 ModelViewMatrix { get; set; }
        public Shader Shader { get; set; }
        public PrimitiveType DrawType { get; set; }
        #endregion

        private int vertexArrayID; //VAO
        private int vertexBuffer; //VBO
        private int colorBuffer; //VBO

        private int matrixID;

        public Mesh2D()
        {
            Vector3[] vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f),
                new Vector3( 0f,  0.8f, 0f)};


            Vector3[] coldata = new Vector3[] { new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f),
                new Vector3( 0f,  1f, 0f)};

            init(vertdata, coldata, Matrix4.Identity);
        }

        public Mesh2D(Vector3[] vertices, Vector3[] colors, Matrix4 modelview = default(Matrix4))
        {
            init(vertices, colors, modelview == default(Matrix4) ? Matrix4.Identity  : modelview);
        }

        private void init(Vector3[] vertices, Vector3[] colors, Matrix4 modelview)
        {
            DrawType = PrimitiveType.Polygon;
            Vertices = vertices;
            Colors = colors;
            ModelViewMatrix = modelview;

            try
            {
                Shader = ShaderManager.GetShader("Default");
            }
            catch (Exception e)
            {
                Console.WriteLine("Mesh2D: Shader not found, load new one from File");
                Shader = ShaderLoader.FromFile("Shader\\vertex.glsl", "Shader\\fragment.glsl");
                ShaderManager.AddShader("Default", Shader);
            }

            GL.GenVertexArrays(1, out vertexArrayID);   //Generate one vertexArrayObject
            GL.BindVertexArray(vertexArrayID);

            matrixID = Shader.GetUniformLocation("modelview");

            GL.GenBuffers(1, out vertexBuffer);
            GL.GenBuffers(1, out colorBuffer);
        }

        public void Prepare()
        {
            //The following command will talk about our 'vertexBuffer' buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //Give the vertices to OpenGL
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * Vector3.SizeInBytes), Vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Colors.Length * Vector3.SizeInBytes), Colors, BufferUsageHint.StaticDraw);
        }

        public void Render()
        {
            Prepare();
            Shader.Begin();

            Matrix4 mvMat = ModelViewMatrix;

            //Send uniform matrix
            GL.UniformMatrix4(matrixID, false, ref mvMat);

            //1st attribute buffer: vertices
            GL.EnableVertexAttribArray(0);  //attribute 0
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            GL.VertexAttribPointer(
                0,                              //attribute 0. No particular reason for 0, but must match the layout in the shader --> layout (location=0)
                3,                              //size
                VertexAttribPointerType.Float,  //type
                false,                          //not normalized
                0,                              //stride
                0                               //array buffer offset
            );

            //2nd attribute buffer: color
            GL.EnableVertexAttribArray(1);
            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
            GL.VertexAttribPointer(
                1,                      //layout(location = 1)
                3,
                VertexAttribPointerType.Float,
                true,
                0,
                0
            );


            //Draw triangle
            GL.DrawArrays(DrawType, 0, Vertices.Length);

            //Clean
            GL.DisableVertexAttribArray(0);
        }
    }
}
