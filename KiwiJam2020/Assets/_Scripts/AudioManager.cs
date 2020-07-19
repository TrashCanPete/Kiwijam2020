using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public bool soundEffectsMuted;
    public Sound[] sounds;
    public Music[] songs;

    //public AudioMixerGroup musicMixer;
    //public AudioMixerGroup soundFXMixer;





    private void Awake()
    {
           foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volumn;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
            s.source.playOnAwake = s.playOnAwake;




            //s.source.outputAudioMixerGroup = soundFXMixer;
        }
        foreach (Music m in songs)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volumn;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;
            m.source.mute = m.mute;

            //m.source.outputAudioMixerGroup = musicMixer;
        }
    }

    private void Start()
    {
        PlayAudio("Engine");
        PlayMusic("Theme");
    }
    public void PlayAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }


    public void StopPlayingAudio(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }
    public void PlayMusic(string name)
    {
        Music m = Array.Find(songs, songs => songs.name == name);
        if (m == null)
        {
            return;
        }
        m.source.Play();
    }


    public void StopPlayingMusic(string name)
    {
        Music m = Array.Find(songs, songs => songs.name == name);
        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        m.source.Stop();
    }
    /*
    public void ClickButton()
    {
        PlayAudio("Button Click");
        PlayAudio("Play Button");
    }
    */
}
