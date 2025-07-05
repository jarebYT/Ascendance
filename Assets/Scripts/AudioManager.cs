using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--------Audio Sources--------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("--------Audio Clip--------")]
    public AudioClip background;
    public AudioClip boss_hurt;
    public AudioClip boss_attack;
    public AudioClip boss_voice;
    public AudioClip boss_voice2;
    public AudioClip boss_voice3;
    public AudioClip boss_footsteps;
    public AudioClip chain;
    public AudioClip hero_hurt;
    public AudioClip hero_attack;
    public AudioClip hero_voice;
    public AudioClip music_boss;
    public AudioClip rocks;

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void PlayMusicLoop(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void MusicStop()
    {
        musicSource.Stop();
        musicSource.loop = false;
    }
}
