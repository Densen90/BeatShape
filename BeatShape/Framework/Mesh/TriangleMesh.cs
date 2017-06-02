using System;
using OpenTK;
using System.Collections.Generic;

namespace BeatShape.Framework
{
    class TriangleMesh : Mesh2D
    {
        public float LineWidth { get; private set; }
        public Box2D Box { get; private set; }

        public TriangleMesh(float width = 0.5f, float lineWidth = 0.1f)
        {
            LineWidth = lineWidth;
            float height = (float) Math.Sqrt(Math.Pow(width, 2f) - Math.Pow(width / 2f, 2f));   //calculate height of a equilateral triangle
            Box = new Box2D(-width / 2f, -height / 2f, width, height);

            Vector3[] vertices;
            Vector3[] col;
            createMesh(out vertices, out col);

            init(vertices, col, Matrix4.Identity);
        }

        private void createMesh(out Vector3[] verts, out Vector3[] cols)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> col = new List<Vector3>();

            ///Outer triangle
            vertices.Add(new Vector3(Box.X, Box.Y, 0f));    //lower left corner
            vertices.Add(new Vector3(Box.MaxX, Box.Y, 0f)); //lower right corner
            vertices.Add(new Vector3((Box.MaxX + Box.X) / 2f, Box.MaxY, 0f));   //upper middle

            /////Inner triangle
            //vertices.Add(new Vector3(Box.X + LineWidth, Box.Y + LineWidth, 0f));
            //vertices.Add(new Vector3(Box.MaxX - LineWidth, Box.Y + LineWidth, 0f));
            //vertices.Add(new Vector3(Box.X + LineWidth, Box.MaxY - LineWidth, 0f));

            for (int i = 0; i < vertices.Count; i++) col.Add(new Vector3(1f, 1f, 0f));

            verts = vertices.ToArray();
            cols = col.ToArray();
        }
    }
}
