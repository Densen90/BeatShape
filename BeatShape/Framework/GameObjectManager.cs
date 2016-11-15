using System;
using System.Collections.Generic;
using System.Linq;

namespace BeatShape.Framework
{
    class GameObjectManager
    {
        private static Dictionary<Guid, GameObject> allObjects = new Dictionary<Guid, GameObject>();

        public static void Add(GameObject obj)
        {
            allObjects.Add(obj.InstaceID, obj);
        }

        public static void Remove(GameObject obj)
        {
            if (allObjects.ContainsKey(obj.InstaceID)) allObjects.Remove(obj.InstaceID);
        }

        public static void PrintAll()
        {
            for(int i=0; i<allObjects.Count; i++)
            {
                var kvp = allObjects.ElementAt(i);
                Console.WriteLine("ID: " + kvp.Key + ", Name: " + kvp.Value.Name);
            }
        }
    }
}
