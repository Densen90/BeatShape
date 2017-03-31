using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    class CircleMesh : Mesh2D
    {
        public float Radius { get; private set; }
        public int Segments { get; private set; }

        public CircleMesh(float radius = 0.1f, int segments = 30) : base()
        {
            Radius = radius;
            Segments = segments;

            Vector3[] vertices;
            Vector3[] col;
            createMesh(out vertices, out col);

            init(vertices, col, Matrix4.Identity);
        }

        private void createMesh(out Vector3[] verts, out Vector3[] cols)
        {
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> col = new List<Vector3>();

            for (int a = 0; a < 360; a += 360 / Segments)
            {
                float heading = a * (float)Math.PI / 180f;
                Vector3 v = new Vector3((float)Math.Cos(heading) * Radius, (float)Math.Sin(heading) * Radius, 0);
                vertices.Add(v);
                col.Add(new Vector3(0, 1, 1));
            }

            verts = vertices.ToArray();
            cols = col.ToArray();
        }
    }
}
