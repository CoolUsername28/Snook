using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log(level)* 20f);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(level) * 20f);
    }

    public void SetFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log(level) * 20f);
    }
}
