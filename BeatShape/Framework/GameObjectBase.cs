using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    abstract class GameObjectBase
    {
        public Mesh2D Mesh { get; set; }

        public GameObjectBase(Mesh2D mesh = null)
        {
            Mesh = mesh ?? new Mesh2D();
        }

        public virtual void Update()
        {
            Mesh.Prepare();
        }

        public virtual void Render()
        {
            Mesh.Render();
        }
    }
}
