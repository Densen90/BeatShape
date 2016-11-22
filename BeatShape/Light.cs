﻿using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatShape
{
    class Light
    {
        public Vector2 location;
        public float red;
        public float green;
        public float blue;

        public Light(Vector2 location, float red, float green, float blue)
        {
            this.location = location;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }
    }
}