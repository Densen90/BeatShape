using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;
using OpenTK;

namespace BeatShape.Framework
{
    class GameObjectManager
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        public IEnumerable<GameObject> GameObjects { get { return gameObjects; } }

        int iterations = 100;
        public GameObjectManager()
        {
            for(int i=-iterations/2; i<=iterations/2; i++)
            {
                for (int j = -iterations / 2; j <= iterations / 2; j++)
                {
                    var p = new Player();
                    p.Position = new Vector3(i, j, 0f) * 0.02f;
                    gameObjects.Add(p);
                }
            }
            
        }

        public void Render()
        {
            foreach (var behaviour in gameObjects.OfType<IBehaviour>())
            {
                behaviour.Render();
            }
        }

        public void Update()
        {
            foreach (var behaviour in gameObjects.OfType<IBehaviour>())
            {
                behaviour.Update();
            }
        }

        public void KeyDown(KeyboardKeyEventArgs e)
        {
            foreach (var control in gameObjects.OfType<IControllable>())
            {
                control.OnKeyDown(e);
            }
        }

        public void KeyUp(KeyboardKeyEventArgs e)
        {
            foreach (var control in gameObjects.OfType<IControllable>())
            {
                control.OnKeyUp(e);
            }
        }

        public void KeyPress(KeyPressEventArgs e)
        {
            foreach (var control in gameObjects.OfType<IControllable>())
            {
                control.OnKeyPress(e);
            }
        }
    }
}
