using UnityEngine;

public class SoundManagement : MonoBehaviour
{
    public static SoundManagement Instance;

    [Header("Audio Sources")]
    public AudioSource uiSource;
    public AudioSource sfxSource;

    [Header("UI Sound")]
    public AudioClip ButtonClick;
    public AudioClip BuyGallo;
    public AudioClip DontBuyGallo;
    public AudioClip GameOver;
    public AudioClip Win;
    public AudioClip MainMenu;

    [Header("Game Sound")]
    public AudioClip GalloShoot;
    public AudioClip GalloHurt;
    public AudioClip EnemyDie;
    public AudioClip NewWave;
    public AudioClip TowerPlace;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayUI(AudioClip clip)
    {
        uiSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}


