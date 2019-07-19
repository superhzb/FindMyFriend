using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager:MonoBehaviour
{
    public AudioSource[] audiosourceArray; //0-BG Music, 1-SoundFX

    public AudioClip bgMusicClip;
    public AudioClip[] soundFXClips; //0-Button, 1-CorrectPick, 2-IncorrectPick, 3-CreateNPC

    private bool canPlayBGMusic = true;
    private bool canPlaySoundFX = true;


    public void PlayBGMusic(AudioClip clip)
    {
        if (!canPlayBGMusic) return; 

        if (!audiosourceArray[0].isPlaying || audiosourceArray[0].clip != clip)
        {
            audiosourceArray[0].clip = clip;
            audiosourceArray[0].Play();
        }
    }

    public void PlayBGMusic()
    {
        if (!canPlayBGMusic) return;

        if (!audiosourceArray[0].isPlaying || audiosourceArray[0].clip != bgMusicClip)
        {
            audiosourceArray[0].clip = bgMusicClip;
            audiosourceArray[0].Play();
        }
    }

    public void PlaySoundFX(AudioClip clip)
    {
        if (!canPlaySoundFX) return;

        audiosourceArray[1].PlayOneShot(clip);
    }

    public void PlayButtonAudioClip()
    {
        PlaySoundFX(soundFXClips[0]);
    }

    public void PlayCorrectClip()
    {
        PlaySoundFX(soundFXClips[1]);
    }

    public void PlayIncorrectClip()
    {
        PlaySoundFX(soundFXClips[2]);
    }

    public void PlayCreateNPCClip()
    {
        AudioSource.PlayClipAtPoint(soundFXClips[3], new Vector3(0, 1, -10)); //Camera position
    }

}
