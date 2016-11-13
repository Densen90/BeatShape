using BeatShape.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape
{
    class TestObject : GameObject
    {
        public TestObject(string name = "", Mesh2D m = null) : base(m)
        {
            this.Name = name;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
