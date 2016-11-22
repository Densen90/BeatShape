using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape.Framework
{
    class Block
    {
        public float x, y, width, height;

        public Block(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public Vector2[] getVertices()
        {
            return new Vector2[] {
                new Vector2(x, y),
                new Vector2(x, y + height),
                new Vector2(x + width, y + height),
                new Vector2(x + width, y) };
        }
    }
}
