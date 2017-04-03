using OpenTK;
using System;

namespace BeatShape.Framework
{
    abstract class GameObject : IBehaviour, IDisposable
    {
        public Mesh2D Mesh { get; set; }
        public string Name { get; set; }

        public Guid InstanceID { get; private set; }

        private Vector3 position = new Vector3(0, 0, 0);
        public Vector3 Position
        {
            get { return position; }
            set
            {
                Vector3 diff = Vector3.Subtract(value, position);
                Translate(new Vector2(diff.X, diff.Y));
            }
        }

        public GameObject(Mesh2D mesh = null)
        {
            this.Mesh = mesh;
            InstanceID = System.Guid.NewGuid();
        }

        public void Dispose()
        {
            Mesh?.Dispose();
        }

        public virtual void Update()
        {
        }

        public virtual void Render()
        {
            Mesh?.Render();
        }

        public void Scale(Vector2 scale)
        {
            Vector3 scaleV = new Vector3(scale.X, scale.Y, 1);
            for (int i = 0; i < Mesh.Vertices.Length; i++)
            {
                Mesh.Vertices[i] = Vector3.Multiply(Mesh.Vertices[i], scaleV);
            }
        }

        public void Translate(Vector2 translate)
        {
            Vector3 transformV = new Vector3(translate.X, translate.Y, 0);
            for (int i = 0; i < Mesh.Vertices.Length; i++)
            {
                Mesh.Vertices[i] = Vector3.Add(Mesh.Vertices[i], transformV);
            }

            position = Vector3.Add(position, new Vector3(translate.X, translate.Y, 0f));
        }
    }
}
