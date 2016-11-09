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
        public TestObject(string name = "")
        {
            this.Name = name;
            Console.WriteLine("Creating: " + this.Name);
        }

        public override void Update()
        {
            base.Update();
            Console.WriteLine("Update " + this.Name);
        }

        public override void Render()
        {
            base.Render();
        }
    }
}
