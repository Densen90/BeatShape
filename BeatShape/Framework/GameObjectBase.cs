using System;

namespace BeatShape.Framework
{
    abstract class GameObject : IBehaviour, IDisposable
    {
        public Mesh2D Mesh { get; set; }
        public string Name { get; set; }

        public Guid InstaceID { get; private set; }

        public GameObject(Mesh2D mesh = null)
        {
            Mesh = mesh ?? new Mesh2D();
            InstaceID = System.Guid.NewGuid();

            GameObjectManager.Add(this);

            EventDispatcher.AddListener("UpdateBehaviour", Update);
            EventDispatcher.AddListener("RenderBehaviour", Render);
        }

        public void Dispose()
        {
            EventDispatcher.RemoveListener("UpdateBehaviour", Update);
            EventDispatcher.RemoveListener("RenderBehaviour", Render);
        }

        public virtual void Update()
        {
        }

        public virtual void Render()
        {
            Mesh.Render();
        }

        public void OnCollision(GameObject other)
        {
            throw new NotImplementedException();
        }
    }
}
