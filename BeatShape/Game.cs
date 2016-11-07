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

namespace BeatShape
{
    class Game : GameWindow
    {
        GameObject obj;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            obj = new GameObject();
            this.Title = "Hello OpenTK";

            GL.ClearColor(Color.CornflowerBlue);
        }

        //draw
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            obj.Render();

            GL.Flush();

            SwapBuffers();
        }

        //Send to shader
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            obj.Update();
        }
    }
}
