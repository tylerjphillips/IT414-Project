using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Singleton
{
    public class Timer : MonoBehaviour
    {
        // Start is called before the first frame update
        public Text timerText;
        ScoreSingleton scoreSingleton = new ScoreSingleton();


    void Start(){scoreSingleton.Value = Time.time;}
        void Update()
        {
            float t = Time.time - scoreSingleton.Value;
            string seconds = t.ToString("f0");
            timerText.text = seconds;
        }
    }

}