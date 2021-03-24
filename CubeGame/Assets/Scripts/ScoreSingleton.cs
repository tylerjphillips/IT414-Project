using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Singleton
{

    public class ScoreSingleton
    {
        public static ScoreSingleton Instance { get; private set; }

        public float Value;

        private ScoreSingleton Awake() 
        {
            if (Instance == null)
            {
                Instance = this;
                return Instance;
            }
            else
            {
                return Instance;
            }
        }
    }
}
