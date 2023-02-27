using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;

namespace Sakuplay.Narradho.Framework
{
    public class Connector:MonoBehaviour
    {
        public static List<Object> objects = new List<Object>();

        private void Start()
        {
            Connections();
            Connect();
        }

        public void Connections()
        {
            objects = new List<object>()
            {
                FindObjectOfType<DialogueEditor>()
            };
        }

        public void Connect()
        {
            var initializables  = GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IInitializable>();

            foreach (var initializable in initializables)
            {
                initializable.Initialize(objects);
            }
        }
    }
}