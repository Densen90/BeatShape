using OpenTK;
using System;

namespace BeatShape.Framework
{
    abstract class GameObject : IBehaviour, IDisposable
    {
        public Mesh2D Mesh { get; set; }
        public string Name { get; set; }

        public Guid InstanceID { get; private set; }

        private Vector2 position = new Vector2(0, 0);
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                Matrix4 outMat;
                Matrix4.CreateTranslation(value.X, value.Y, 0f, out outMat);
                Mesh.TranslationMatrix = outMat;
            }
        }

        private float rotation = 0f;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                Matrix4 outMat;
                Matrix4.CreateRotationZ(value, out outMat);
                //Matrix4.CreateTranslation(value.X, value.Y, 0f, out outMat);
                Mesh.RotationMatrix = outMat;
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
            Position = Vector2.Add(position, translate);
        }
    }
}
