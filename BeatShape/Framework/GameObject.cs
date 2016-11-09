using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    abstract class GameObject : IBehaviour, IDisposable
    {
        public Mesh2D Mesh { get; set; }
        public string Name { get; set; }

        public GameObject(Mesh2D mesh = null)
        {
            Console.WriteLine("Create " + Name);
            Mesh = mesh ?? new Mesh2D();
            EventDispatcher.AddListener("UpdateBehaviour", Update);
            EventDispatcher.AddListener("RenderBehaviour", Render);
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose " + Name);
            EventDispatcher.RemoveListener("UpdateBehaviour", Update);
            EventDispatcher.RemoveListener("RenderBehaviour", Render);
        }

        public virtual void Update()
        {
            Mesh.Prepare();
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
