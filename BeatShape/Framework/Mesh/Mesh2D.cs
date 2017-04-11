using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace BeatShape.Framework
{
    abstract class Mesh2D : IDisposable
    {
        #region object data
        private Vector3[] vertices;
        public Vector3[] Vertices
        {
            get { return vertices; }
            internal set { vertices = value; changed = true; }
        }

        private Vector3[] colors;
        public Vector3[] Colors
        {
            get { return colors; }
            internal set { colors = value; changed = true; }
        }

        public Matrix4 TranslationMatrix { get; internal set; }
        public Matrix4 RotationMatrix { get; internal set; }

        public Matrix4 ModelViewMatrix { get; internal set; }
        public PrimitiveType DrawType { get; internal set; }
        public Matrix4 ScaleAdjust { get; private set; }

        public Shader Shader { get; set; }
        #endregion

        private int vertexArrayID; //VAO
        private int vertexBuffer; //VBO
        private int colorBuffer; //VBO

        private int matrixID;
        private bool changed = false;

        public Mesh2D()
        {
        }

        //public Mesh2D(Vector3[] vertices, Vector3[] colors, Matrix4 modelview = default(Matrix4))
        //{
        //    init(vertices, colors, modelview == default(Matrix4) ? Matrix4.Identity  : modelview);
        //}

        public void Dispose()
        {
            EventDispatcher.RemoveListener("ResizeScreen", AdjustScalematrix);
        }

        protected void init(Vector3[] vertices, Vector3[] colors, Matrix4 modelview)
        {
            EventDispatcher.AddListener("ResizeScreen", AdjustScalematrix);
            TranslationMatrix = Matrix4.Identity;
            RotationMatrix = Matrix4.Identity;
            AdjustScalematrix();
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

            //Prepare only once at initialization, or when vertices change
            Prepare();
        }

        public void Prepare()
        {
            //The following command will talk about our 'vertexBuffer' buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
            //Give the vertices to OpenGL
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * Vector3.SizeInBytes), Vertices, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Colors.Length * Vector3.SizeInBytes), Colors, BufferUsageHint.StaticDraw);
            changed = false;
        }

        public void Render()
        {
            if (changed) Prepare();
            Shader.Begin();

            Matrix4 mvMat = ModelViewMatrix * RotationMatrix * ScaleAdjust * TranslationMatrix;

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

        private void AdjustScalematrix()
        {
            float aspect = Screen.Width / Screen.Height;
            ScaleAdjust = new Matrix4(new Vector4(aspect > 1 ? 1 : aspect, 0, 0, 0), new Vector4(0, aspect > 1 ? aspect : 1, 0, 0), new Vector4(0, 0, 1, 0), new Vector4(0, 0, 0, 1));
        }
    }
}
