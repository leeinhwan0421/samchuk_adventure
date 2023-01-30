using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SoundInstance : MonoBehaviour
{
    private static SoundInstance instance;
    public static SoundInstance Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    [Header("BGM")]
    [SerializeField] private List<AudioClip> bgm = new List<AudioClip>();
    [SerializeField] private List<AudioClip> stageBgm = new List<AudioClip>();
    [Header("SFX")]
    [SerializeField] private List<AudioClip> playerSfx = new List<AudioClip>();
    [SerializeField] private List<AudioClip> enemySfx = new List<AudioClip>();
    [SerializeField] private List<AudioClip> normalSfx = new List<AudioClip>();

    private AudioSource audioSource;

    public static float sfxValue = 1;
    public static float musicValue = 1;

    private void InstantiateAudioObject(AudioClip clip)
    {
        GameObject audioObject = new GameObject("audioObject");

        AudioSource source = audioObject.AddComponent<AudioSource>();

        source.clip = clip;
        source.loop = false;
        source.volume = sfxValue;

        source.Play();

        Destroy(audioObject, clip.length);
    }

    public void ChangeBGM(int key)
    {
        if (audioSource.clip == bgm[key]) return;

        if (key == 0)
            audioSource.volume = 1.0f * musicValue;
        else
            audioSource.volume = 0.3f * musicValue;

        audioSource.clip = bgm[key];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void ChangeBGMSoundValue(float value)
    {
        musicValue = value;

        if (SceneManager.GetActiveScene().name != "GameScene" && SceneManager.GetActiveScene().name != "HowToPlayScene")
            audioSource.volume = 1.0f * musicValue;
        else
            audioSource.volume = 0.3f * musicValue;
    }

    public void ChangeStageBGM(int key)
    {
        if (audioSource.clip == stageBgm[key]) return;

        audioSource.clip = stageBgm[key];
        audioSource.loop = true;
        audioSource.Play();
    }

    public void InstantiatePlayerSFX(int key)
    {
        if (playerSfx[key] == null) return;

        InstantiateAudioObject(playerSfx[key]);
    }

    public void InstantiateEnemySFX(int key)
    {
        if (enemySfx[key] == null) return;

        InstantiateAudioObject(enemySfx[key]);
    }

    public void InstantiateNormalSFX(int key)
    {
        if (normalSfx[key] == null) return;

        if (normalSfx[key].name == "GAMEOVER") audioSource.volume = 0.05f;

        InstantiateAudioObject(normalSfx[key]);
    }
}
