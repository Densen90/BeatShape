﻿using BeatShape.Framework;
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
        private GameObjectManager goManager;

        public Game(int width, int height) : base(width, height, new GraphicsMode(32, 24, 0, 4))//anti alisaing
        {
            Screen.Width = width;
            Screen.Height = height;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("Load Game");

            this.Title = "BeatShape";
            goManager = new GameObjectManager();

            Console.WriteLine("Loading finished");
            GL.ClearColor(Color.CornflowerBlue);
        }

        //draw
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            goManager.Render();

            //Console.WriteLine("FPS: " + 1f / e.Time);

            GL.Flush();

            SwapBuffers();
        }

        //Send to shader
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            goManager.Update();
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            //only fire this event once, when first is pressed and not repeatedly
            if (!e.IsRepeat) goManager.KeyDown(e);

            if (e.Key == Key.Escape) Close();
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (!e.IsRepeat) goManager.KeyUp(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            goManager.KeyPress(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            Screen.Width = Width;
            Screen.Height = Height;
            EventDispatcher.Invoke("ResizeScreen");
        }
    }
}
