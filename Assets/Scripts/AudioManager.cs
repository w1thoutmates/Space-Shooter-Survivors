using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource musicSource;
    public AudioSource[] sfxSources;
    private int sfxIndex = 0;

    public float musicVolume = 0.5f;
    public float sfxVolume = 0.5f;

    private AudioLowPassFilter lowPassFilter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        lowPassFilter = musicSource.GetComponent<AudioLowPassFilter>();
        if (lowPassFilter == null)
            lowPassFilter = musicSource.gameObject.AddComponent<AudioLowPassFilter>();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float v = 0.2f)
    {
        AudioSource src = sfxSources[sfxIndex];
        src.PlayOneShot(clip, v);
        sfxIndex = (sfxIndex + 1) % sfxSources.Length;
    }

    public void SetSFXVolume(float v)
    {
        sfxVolume = v;
    }

    public void MuteMusicAndAddFilter()
    {
        musicSource.volume *= 0.2f;
        lowPassFilter.enabled = true;
        lowPassFilter.cutoffFrequency = 800f; 
    }

    public void RestoreMusicSettings()
    {
        musicSource.volume = musicVolume;
        lowPassFilter.enabled = false;
    }
}
