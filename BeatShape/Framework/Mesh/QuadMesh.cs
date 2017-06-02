using OpenTK;
using System.Collections.Generic;

namespace BeatShape.Framework
{
    class QuadMesh : Mesh2D
    {
        public Box2D Box { get; private set; }

        public QuadMesh(float width = 0.5f, float height = 0.5f) : base()
        {
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

            vertices.Add(new Vector3(Box.MaxX, Box.MaxY, 0f));
            col.Add(new Vector3(0, 1, 1));

            vertices.Add(new Vector3(Box.X, Box.MaxY, 0f));
            col.Add(new Vector3(0, 1, 1));

            vertices.Add(new Vector3(Box.X, Box.Y, 0f));
            col.Add(new Vector3(0, 1, 1));

            vertices.Add(new Vector3(Box.MaxX, Box.Y, 0f));
            col.Add(new Vector3(0, 1, 1));

            verts = vertices.ToArray();
            cols = col.ToArray();
        }
    }
}
