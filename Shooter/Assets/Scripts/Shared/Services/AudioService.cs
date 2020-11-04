using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Shared {

    /**
     * Plays audio clips from an audio theme.
     */
    public class AudioService: MonoBehaviour {

        /** Audio clip collection */
        public AudioTheme theme;

        /** Singleton service instance */
        private static AudioService service;

        /** Dictionary of the theme's audio clips */
        private static Dictionary<string, AudioClip> clips;


        /**
         * Makes the service a singleton.
         */
        private void Awake() {
            if (service == null) {
                DontDestroyOnLoad(gameObject);
                clips = theme.events.ToDictionary(i => i.name, i => i.clip);
                service = this;
            } else if (service != this) {
                Destroy(gameObject);
            }
        }


        /**
         * Plays an audio loop on a game object's audio source.
         */
        public static void PlayLoop(GameObject o, string name) {
            if (clips.ContainsKey(name) && clips[name] != null) {
                AudioSource source = o.GetComponentInChildren<AudioSource>();
                source.loop = true;
                source.clip = clips[name];
                source.Play();
            }
        }


        /**
         * Stop the audio loop that is playing on a game object.
         */
        public static void StopLoop(GameObject o) {
            AudioSource source = o.GetComponentInChildren<AudioSource>();
            source.Stop();
        }


        /**
         * Plays a sound event on a game object's audio source.
         */
        public static void PlayOneShot(GameObject o, string name) {
            if (clips.ContainsKey(name) && clips[name] != null) {
                AudioSource source = o.GetComponentInChildren<AudioSource>();
                source.PlayOneShot(clips[name]);
            }
        }
    }
}
