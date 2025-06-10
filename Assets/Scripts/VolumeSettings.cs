using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer; // Reference to the AudioMixer
    [SerializeField] private Slider musicSlider; // Slider for music volume
    [SerializeField] private Slider SFXSlider; // Slider for music volume


    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume(); // Load the saved volume if it exists
        }
        else
        {
            setMusicVolume(); // Set the initial volume when the script starts
            setSFXVolume(); // Set the initial SFX volume
        }
    }

    public void setMusicVolume()
    {
        float volume = musicSlider.value; // Get the value from the slider
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20); // Set the volume in the AudioMixer
        PlayerPrefs.SetFloat("musicVolume", volume); // Save the volume setting
    }

    public void setSFXVolume()
    {
        float volume = SFXSlider.value; // Get the value from the slider
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20); // Set the volume in the AudioMixer
        PlayerPrefs.SetFloat("SFX", volume); // Save the volume setting
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        setMusicVolume(); // Apply the loaded volume

        SFXSlider.value = PlayerPrefs.GetFloat("SFX");
        setSFXVolume(); // Apply the loaded SFX volume
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
