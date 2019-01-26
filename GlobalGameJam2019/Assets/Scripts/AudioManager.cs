using UnityEngine.Audio;
using System;
using UnityEngine;

[Serializable]
public class AudioManager : MonoBehaviour {
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start() {
        Play("Theme");
    }

    private Sound GetSound(string name) {
        Sound sound = Array.Find(sounds, s => s.name == name);
        if (sound == null) {
            Debug.LogWarning("Sound could not be found: " + name);
        }
        return sound;
    }

    public void Play(string name) {
        var sound = GetSound(name);
        sound.source.Play();
    }

    public void PlayPitch(string name, float pitch) {
        var sound = GetSound(name);
        sound.pitch = pitch;
        sound.source.Play();
    }

    public void PlayPitchRandom(string name, float maxDeviation = 1.0f) {
        var sound = GetSound(name);
        float randPitch = UnityEngine.Random.Range(sound.pitch - maxDeviation, sound.pitch + maxDeviation);
        sound.pitch = randPitch;
        sound.source.Play();
    }
}
