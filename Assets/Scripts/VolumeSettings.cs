using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer myMixer;
    public Slider musicSlider; 
    public Slider SFXSlider;


    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume(); 
        }
        else
        {
            setMusicVolume(); 
            setSFXVolume(); 
        }
    }

    public void setMusicVolume()
    {
        float volume = musicSlider.value; 
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("musicVolume", volume); 
    }

    public void setSFXVolume()
    {
        float volume = SFXSlider.value; 
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20); 
        PlayerPrefs.SetFloat("SFX", volume); 
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        setMusicVolume(); 

        SFXSlider.value = PlayerPrefs.GetFloat("SFX");
        setSFXVolume(); 
    }

    public void muteToggle(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }
}
