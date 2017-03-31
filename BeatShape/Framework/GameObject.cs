using System;

namespace BeatShape.Framework
{
    abstract class GameObject : IBehaviour, IDisposable
    {
        public Mesh2D Mesh { get; set; }
        public string Name { get; set; }

        public Guid InstanceID { get; private set; }

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
    }
}
