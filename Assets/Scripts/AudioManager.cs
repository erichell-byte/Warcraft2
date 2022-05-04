using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip[] clipsWalk;
        public AudioClip[] clipsSelected;
        public AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

        }

        public void PlayAudioWalk(int i)
        {
            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(clipsWalk[i]);
        }

        public void PlayAudioSelected(int i)
        {
            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(clipsSelected[i]);
        }
    }
}