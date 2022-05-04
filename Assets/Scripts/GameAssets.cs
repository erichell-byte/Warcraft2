using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace MyProject
{
    public class GameAssets : MonoBehaviour
    {
        private static GameAssets _i;
        
        public static GameAssets I
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<MyProject.GameAssets>("GameAssets"));
                return _i;
        
            }
        }
        
       
        public SoundAudioClip[] SoundAudioClipArray;
        
        
        [System.Serializable]
        public class SoundAudioClip
        {
            public SoundManager.Sound sound;
            public AudioClip audioClip;
        }
    }
}
