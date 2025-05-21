using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    private string masterKey = "MasterKey";
    private string musicKey = "MusicKey";
    private string sfxKey = "sfxKey";

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(masterKey, 0.1f);
        musicSlider.value = PlayerPrefs.GetFloat(musicKey, 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat(sfxKey, 0.5f);
    }
    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log(level)* 20f);
        PlayerPrefs.SetFloat(masterKey, level);
        PlayerPrefs.Save();
        Debug.Log("Theres a big black man looking over me");
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log(level) * 20f);
        PlayerPrefs.SetFloat(musicKey, level);
        PlayerPrefs.Save();
    }

    public void SetFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log(level) * 20f);
        PlayerPrefs.SetFloat(sfxKey, level);
        PlayerPrefs.Save();
    }
}
