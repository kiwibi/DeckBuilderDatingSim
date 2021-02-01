using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    static AudioPlayer instance_;

    private AudioSource[] source_;

    public AudioClip[] SoundEffects_;
    public AudioClip[] VoiceLines_;
    public AudioClip[] Results_;
    void Start()
    {
        instance_ = this;
        source_ = GetComponentsInChildren<AudioSource>();
    }

    public static void PlaySoundEffect(int index)
    {
        if (instance_.source_[0].isPlaying)
            StopPlayingEffects();

        instance_.source_[0].clip = instance_.SoundEffects_[index - 1];
        instance_.source_[0].Play();
    }

    public static void PlayVoiceLine(int index)
    {
        if (instance_.source_[1].isPlaying)
            StopPlayingVoice();

        instance_.source_[1].clip = instance_.VoiceLines_[index - 1];
        instance_.source_[1].Play();
    }

    public static void PlayResult(int index)
    {
        if (instance_.source_[2].isPlaying)
            StopPlayingResults();

        instance_.source_[2].clip = instance_.Results_[index - 1];
        instance_.source_[2].Play();
    }

    public static void StopPlayingEffects()
    {
        instance_.source_[0].Stop();
    }

    public static void StopPlayingVoice()
    {
        instance_.source_[1].Stop();
    }
    public static void StopPlayingResults()
    {
        instance_.source_[2].Stop();
    }

}
