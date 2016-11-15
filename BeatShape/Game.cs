using BeatShape.Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Input;
using OpenTK.Graphics;

namespace BeatShape
{
    class Game : GameWindow
    {
        public Game(int width, int height) :base(width, height, new GraphicsMode(32, 24, 0, 4))//anti alisaing
        {}
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("Load Game");

            this.Title = "Hello OpenTK";

            /*
            Vector3[] vertdata = new Vector3[] { new Vector3(0.3f, 0.5f, 0f),
                    new Vector3( -0.3f, 0.5f, 0f),
                    new Vector3( -0.3f, -0.5f, 0f),
                    new Vector3( 0.3f, -0.5f, 0f)};

            Vector3[] coldata = new Vector3[] { new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 0f), new Vector3(1f, 1f, 0f) };
            Mesh2D m = new Mesh2D(vertdata, coldata);
            var obj = new TestObject("Object One", m);

            vertdata = new Vector3[] { new Vector3(0.5f, 0.8f, 0f),
                    new Vector3( -0.2f, 0.8f, 0f),
                    new Vector3( -0.2f, -0.2f, 0f),
                    new Vector3( 0.5f, -0.2f, 0f)};

            coldata = new Vector3[] { new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f) };
            m = new Mesh2D(vertdata, coldata);
            var obj2 = new TestObject("Object One", m);

            obj.Layer = 1;
            obj2.Layer = 0;*/

            
            float max_x = 10;
            float max_y = 10;
            int count = 0;
            for(float x=0f; x<max_x; x++)
            {
                for(float y=0f; y<max_y; y++)
                {
                    float width = 2f / max_x;
                    float height = 1f / max_y;
                    Vector3[] vertdata = new Vector3[] { new Vector3(-1f + 2*x/max_x, 1f - height - 2*y/max_y, 0f),
                        new Vector3( -1f + (width/2f) + 2*x/max_x, 1f -  2*height - 2*y/max_y, 0f),
                        new Vector3( -1f + width + 2*x/max_x, 1f - height - 2*y/max_y, 0f),
                        new Vector3( -1f + (width/2f) + 2*x/max_x,  1f - 2*y/max_y, 0f) };


                    Vector3[] coldata = new Vector3[] { new Vector3(1f), new Vector3(1f), new Vector3(1f), new Vector3(1f) };

                    Mesh2D m = new Mesh2D(vertdata, coldata);

                    count++;
                    Console.WriteLine(count);
                    var obj = new TestObject("Object: " + count, m);
                }
            }

            /*
            List<Vector3> vertData = new List<Vector3>();
            List<Vector3> colData = new List<Vector3>();
            for (float x = -1.139f; x <= 1.139; x += 0.001f)
            {
                float delta = cbrt(x * x) * cbrt(x * x) - 4f * x * x + 4f;
                float y1 = (cbrt(x * x) + (float)Math.Sqrt(delta)) / 2f;
                float y2 = (cbrt(x * x) - (float)Math.Sqrt(delta)) / 2f;
                vertData.Add(new Vector3(x, y1, 0f) * 0.8f);
                vertData.Add(new Vector3(x, y2, 0f) * 0.8f);
                colData.Add(new Vector3(1f, 0f, 0f));
                colData.Add(new Vector3(1f, 0f, 0f));
            }

            Mesh2D mm = new Mesh2D(vertData.ToArray(), colData.ToArray());
            var obj = new TestObject("Heart", mm);*/

            Console.WriteLine("Loading finished");
            GL.ClearColor(Color.ForestGreen);
        }

        //draw
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Enable(EnableCap.DepthTest);
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

        float cbrt(double x)
        {
            return (float)Math.Pow(x, (1.0 / 3.0));
        }
    }
}
