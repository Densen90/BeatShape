using BeatShape.Framework;
using OpenTK;
using OpenTK.Graphics.ES20;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace BeatShape
{
    class Game : GameWindow
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("Load Game");

            this.Title = "Hello OpenTK";

            float max_x = 10;
            float max_y = 10;
            int count = 0;
            for(float x=0f; x<max_x; x++)
            {
                for(float y=0f; y<max_y; y++)
                {
                    float width = 2f / max_x;
                    float height = 2f / max_y;
                    Vector3[] vertdata = new Vector3[] { new Vector3(-1f + 2*x/max_x, 1f - height - 2*y/max_y, 0f),
                    new Vector3( -1f + width + 2*x/max_x, 1f - height - 2*y/max_y, 0f),
                    new Vector3( -1f + (width/2f) + 2*x/max_x,  1f - 2*y/max_y, 0f)};


                    Vector3[] coldata = new Vector3[] { new Vector3(1f), new Vector3(1f), new Vector3(1f) };

                    Mesh2D m = new Mesh2D(vertdata, coldata);

                    count++;
                    var obj = new TestObject("Object: " + count, m);
                }
            }

            Console.WriteLine("Loading finished");
            GL.ClearColor(Color.CornflowerBlue);
        }

        //draw
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            EventDispatcher.Invoke("RenderBehaviour");

            Console.WriteLine("FPS: " + 1f / e.Time);

            GL.Flush();

            SwapBuffers();
        }

        //Send to shader
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            EventDispatcher.Invoke("UpdateBehaviour");
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape) Close();
        }
    }
}
