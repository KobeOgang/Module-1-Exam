using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSrc;
    [SerializeField] AudioSource SFXsource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip boost;
    

    public static AudioManager instance;

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
    }
    void Start()
    {
        musicSrc.clip = background;
        musicSrc.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXsource.PlayOneShot(clip);
    }
}
