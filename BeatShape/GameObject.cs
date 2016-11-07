using BeatShape.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape
{
    class GameObject : GameObjectBase
    {
        public GameObject()
        {
            Console.WriteLine("Making a new GameObject");
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
