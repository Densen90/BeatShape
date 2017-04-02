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

        public Player() : base()
        {
            this.Mesh = new CircleMesh();
        }

        public override void Update()
        {
            base.Update();
            this.Translate(new Vector2(horizontal * Speed, vertical * Speed));
        }

        public void OnCollision(ICollidable other)
        {
            Console.WriteLine("Collision");
        }

        public void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key.Equals(Key.A)) horizontal = -1f;
            if (e.Key.Equals(Key.D)) horizontal = 1f;
            if (e.Key.Equals(Key.W)) vertical = 1f;
            if (e.Key.Equals(Key.S)) vertical = -1f;
        }

        public void OnKeyPress(KeyPressEventArgs e)
        {
            Console.WriteLine("Key press player");
        }

        public void OnKeyUp(KeyboardKeyEventArgs e)
        {
        }
    }
}
