using UnityEngine;

public class SoundManager : MonoBehaviour           //works as the sound manager
{
    public static SoundManager instance;             //can be called anywhere easily 
    public AudioSource musicSource;

    public AudioClip spinSound;
    public AudioClip spinStopSound;
    public AudioClip winSoundfx;
    public AudioClip jackpotSoundfx;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic(AudioClip music, bool play)
    {
        if (play)
        {
            musicSource.clip = null;
            musicSource.clip = music;
            musicSource.Play();
        }
        else
        {
            musicSource.Stop();
            musicSource.clip = null;
        }
    }

    public void PlaySFX(AudioClip sFX)
    {
        musicSource.PlayOneShot(sFX);
    }

}
