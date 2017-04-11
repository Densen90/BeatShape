using BeatShape.Framework;
using OpenTK;
using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace BeatShape
{
    class Player : GameObject, ICollidable, IControllable
    {
        private float horizontal = 0f;
        private float vertical = 0f;
        private float Speed = 0.01f;

        public Player() : base(new QuadMesh(1f, 1f))
        {
            //this.Mesh = new CircleMesh();
            //this.Mesh = new QuadMesh(0.01f, 0.01f);
        }

        public override void Update()
        {
            base.Update();
            this.Translate(new Vector2(horizontal, vertical) * Speed);
            this.Rotation = (this.Rotation + 0.02f) % 360f;
        }

        public void OnCollision(ICollidable other)
        {
            Console.WriteLine("Collision");
        }

        public void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key.Equals(Key.A)) horizontal -= 1f;
            if (e.Key.Equals(Key.D)) horizontal += 1f;
            if (e.Key.Equals(Key.W)) vertical += 1f;
            if (e.Key.Equals(Key.S)) vertical -= 1f;
        }

        public void OnKeyPress(KeyPressEventArgs e)
        {
        }

        public void OnKeyUp(KeyboardKeyEventArgs e)
        {
            if (e.Key.Equals(Key.A)) horizontal += 1f;
            if (e.Key.Equals(Key.D)) horizontal -= 1f;
            if (e.Key.Equals(Key.W)) vertical -= 1f;
            if (e.Key.Equals(Key.S)) vertical += 1f;
        }
    }
}
