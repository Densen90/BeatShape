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
        private List<Light> lights = new List<Light>();
        private List<Block> blocks = new List<Block>();
        private int fragmentShader;
        private int shaderProgram;
        private bool right;
        private bool down;
        private bool left;
        private bool up;

        public Game(int width, int height) :base(width, height, new GraphicsMode(32, 24, 0, 4))//anti alisaing
        {}

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Console.WriteLine("Load Game " + Width + "x" + Height);

            this.Title = "Hello OpenTK";

            Random r = new Random();
            int lightCount = 1;
            int blockCount = 10;

            for (int i = 1; i <= lightCount; i++)
            {
                Vector2 location = new Vector2((float)r.NextDouble() * Width, (float)r.NextDouble() * Height);
                Console.WriteLine(location);
                lights.Add(new Light(location, (float)r.NextDouble() * 10, (float)r.NextDouble() * 10, (float)r.NextDouble() * 10));
            }

            for (int i = 1; i <= blockCount; i++)
            {
                float width = 50f;
                float height = 50f;
                float x = ((float)r.NextDouble() * (Width - width));
                float y = ((float)r.NextDouble() * (Height - height));
                blocks.Add(new Block(x, y, width, height));
            }

            shaderProgram = GL.CreateProgram();
            fragmentShader = GL.CreateShader(ShaderType.FragmentShader);

            string fragmentShaderSource = ShaderLoader.ShaderStringFromFile("Shader\\fragment.glsl");
            //Console.WriteLine(fragmentShaderSource);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);

            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);
            GL.ValidateProgram(shaderProgram);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, Width, Height, 0, 1, -1);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.Enable(EnableCap.StencilTest);


            Console.WriteLine("Loading finished");
            GL.ClearColor(Color.Black);
        }

        //draw
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            OnJoystickInput(OpenTK.Input.GamePad.GetState(0));

            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach(Light light in lights)
            {
                GL.ColorMask(false, false, false, false);
                GL.StencilFunc(StencilFunction.Always, 1, 1);
                GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);

                //Console.WriteLine(light.location);

                foreach(Block block in blocks)
                {
                    Vector2[] vertices = block.getVertices();
                    for (int i = 0; i < vertices.Length; i++)
                    {
                        Vector2 currentVertex = vertices[i];
                        Vector2 nextVertex = vertices[(i + 1) % vertices.Length];
                        Vector2 edge;
                        Vector2.Subtract(ref nextVertex, ref currentVertex, out edge);
                        Vector2 normal = new Vector2(edge.Y, -edge.X);
                        Vector2 lightToCurrent;
                        Vector2.Subtract(ref currentVertex, ref light.location, out lightToCurrent);
                        if (Vector2.Dot(normal, lightToCurrent) > 0)
                        {
                            Vector2 sub;
                            Vector2.Subtract(ref currentVertex, ref light.location, out sub);
                            sub = Vector2.Multiply(sub, 800);
                            Vector2 point1;
                            Vector2.Add(ref currentVertex, ref sub, out point1);
                            Vector2.Subtract(ref nextVertex, ref light.location, out sub);
                            sub = Vector2.Multiply(sub, 800);
                            Vector2 point2;
                            Vector2.Add(ref nextVertex, ref sub, out point2);

                            GL.Begin(PrimitiveType.Quads);
                            GL.Vertex2(currentVertex.X, currentVertex.Y);
                            GL.Vertex2(point1.X, point1.Y);
                            GL.Vertex2(point2.X, point2.Y);
                            GL.Vertex2(nextVertex.X, nextVertex.Y);
                            GL.End();
                        }
                    }
                }

                GL.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Keep);
                GL.StencilFunc(StencilFunction.Equal, 0, 1);
                GL.ColorMask(true, true, true, true);

                GL.UseProgram(shaderProgram);
                GL.Uniform2(GL.GetUniformLocation(shaderProgram, "lightLocation"), light.location.X, Height - light.location.Y);
                GL.Uniform3(GL.GetUniformLocation(shaderProgram, "lightColor"), light.red, light.green, light.blue);
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

                GL.Begin(PrimitiveType.Quads);
                GL.Vertex2(0, 0);
                GL.Vertex2(0, Height);
                GL.Vertex2(Width, Height);
                GL.Vertex2(Width, 0);
                GL.End();

                GL.Disable(EnableCap.Blend);
                GL.UseProgram(0);
                GL.Clear(ClearBufferMask.StencilBufferBit);
            }

            GL.Color3(0, 0, 0);
            foreach (Block block in blocks)
            {
                GL.Begin(PrimitiveType.Quads);
                foreach (Vector2 vertex in block.getVertices())
                {
                    //Console.WriteLine(vertex);
                    GL.Vertex2(vertex.X, vertex.Y);
                }
                GL.End();
            }

            EventDispatcher.Invoke("RenderBehaviour");

            //Console.WriteLine("FPS: " + 1f / e.Time);

            GL.Flush();

            SwapBuffers();
        }

        private void OnJoystickInput(GamePadState gamePadState)
        {
            if (gamePadState.DPad.IsLeft) left = true;
            else if (left) left = false;
            if (gamePadState.DPad.IsRight) right = true;
            else if (right) right = false;
            if (gamePadState.DPad.IsDown) down = true;
            else if (down) down = false;
            if (gamePadState.DPad.IsUp) up = true;
            else if (up) up = false;

            if (gamePadState.Buttons.A.HasFlag(ButtonState.Pressed))
            {
                for(int i = 0; i<1; i++)
                {
                    Console.WriteLine("Press " + OpenTK.Input.GamePad.SetVibration(0, 0.5f, 0.5f));
                }
                
            }
                
        }

        //Send to shader
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            foreach(var light in lights)
            {
                light.location.X += right ? 5f : 0f;
                light.location.X += left ? -5f : 0f;
                light.location.Y += up ? -5f : 0f;
                light.location.Y += down ? 5f : 0f;

            }

            EventDispatcher.Invoke("UpdateBehaviour");
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Key == Key.Escape) Close();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar.Equals('d')) right = true;
            if (e.KeyChar.Equals('s')) down = true;
            if (e.KeyChar.Equals('a')) left = true;
            if (e.KeyChar.Equals('w')) up = true;
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            base.OnKeyUp(e);

            if (e.Key == Key.D) right = false;
            if (e.Key == Key.S) down = false;
            if (e.Key == Key.A) left = false;
            if (e.Key == Key.W) up = false;
        }
    }
}
