using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MyProject
{
    public class SoundManager
    {
        public enum Sound
        {
            PlayerMove,
            PlayerSelected,
            PlayerDied,
            OrcMove,
            OrcSelected,
            OrcDied
            
        }

        private static Dictionary<Sound, float> soundTimerDictionary;
        private static GameObject oneShotGameObject;
        private static AudioSource oneShotAudioSource;

        public static void Initialize()
        {
            soundTimerDictionary = new Dictionary<Sound, float>();
            soundTimerDictionary[Sound.PlayerMove] = 0f;
            soundTimerDictionary[Sound.OrcMove] = 0f;
        }

        // 3D sound
        public static void PlaySound(Sound sound, Vector3 position)
        {
            if (CanPlaySound(sound))
            {
                
                GameObject soundGameObject = new GameObject("Sound");
                soundGameObject.transform.position = position;
                AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
                audioSource.clip = GetAudioClip(sound);
                audioSource.Play();
                
            }
        }
        // 2D sound
        public static void PlaySound(Sound sound)
        {
            if (CanPlaySound(sound))
            {
                if (oneShotGameObject == null)
                {
                    oneShotGameObject = new GameObject("One Shot Sound");
                    oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
                }
                oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
            }
        }

        private static bool CanPlaySound(Sound sound)
        {
            switch (sound)
            {
                default:
                    return true;
                case Sound.PlayerMove:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float playerMoveTimerMax = 3f;
                        if (lastTimePlayed + playerMoveTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        } else {
                            return false;
                        }
                    } else
                    {
                        return true;
                    }
                case Sound.OrcMove:
                    if (soundTimerDictionary.ContainsKey(sound))
                    {
                        float lastTimePlayed = soundTimerDictionary[sound];
                        float playerMoveTimerMax = 3f;
                        if (lastTimePlayed + playerMoveTimerMax < Time.time)
                        {
                            soundTimerDictionary[sound] = Time.time;
                            return true;
                        } else {
                            return false;
                        }
                    } else
                    {
                        return true;
                    }
            }
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            foreach (GameAssets.SoundAudioClip soundAudioClip in MyProject.GameAssets.I.SoundAudioClipArray)
            {
                if (soundAudioClip.sound == sound)
                    return soundAudioClip.audioClip;
            }
            Debug.LogError("Sound " + sound + " not found!");
            return null;
        }
    }
}
