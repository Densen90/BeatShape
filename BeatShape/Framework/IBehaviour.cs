using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    interface IBehaviour
    {
        void Update();
        void Render();
        void OnCollision(GameObject other);

    }
}
