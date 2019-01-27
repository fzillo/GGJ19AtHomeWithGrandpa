using UnityEngine.Audio;
using System;
using System.Collections;
using UnityEngine;
using System.Linq;

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

            s.clip.LoadAudioData();
        }
    }
    void Start() {
        PlaySequence("intro", "Theme");
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
        sound.source.pitch = pitch;
        sound.source.Play();
    }

    public void PlayPitchRandom(string name, float maxDeviation = 2.0f) {
        var sound = GetSound(name);
        float randPitch = UnityEngine.Random.Range(sound.pitch - maxDeviation, sound.pitch + maxDeviation);
        sound.source.pitch = randPitch;
        sound.source.Play();
    }

    public void PlaySequence(params string[] names) {
        StartCoroutine(_PlaySequence(names));
    }
    private IEnumerator _PlaySequence(params string[] names) {
        foreach (Sound sound in names.Select(n => GetSound(n))) {
            sound.source.Play();
            yield return new WaitForSecondsRealtime(sound.source.clip.length);
        }
    }

    public void PlayInSequencePitchRandom(string name, int count, float maxDeviation = 2.0f) {
        StartCoroutine(_PlayInSequencePitchRandom(name, count, maxDeviation));
    }
    private IEnumerator _PlayInSequencePitchRandom(string name, int count, float maxDeviation = 2.0f) {
        var sound = GetSound(name);
        for (var i = 0; i < count; i++) {
            float randPitch = UnityEngine.Random.Range(sound.pitch - maxDeviation, sound.pitch + maxDeviation);
            Debug.Log(randPitch);
            sound.source.pitch = randPitch;
            sound.source.Play();
            yield return new WaitForSecondsRealtime(sound.source.clip.length);
        }
    }
}
