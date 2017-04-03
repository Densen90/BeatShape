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

        int iterations = 50;
        public GameObjectManager()
        {
            gameObjects.Add(new Player());
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
