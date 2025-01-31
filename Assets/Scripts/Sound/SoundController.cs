using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    private static SoundController instance;

    public static SoundController Instance { get { return instance; } }

    [Header("---------- Audio Source ----------")]
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource sfx;

    [Header("---------- Audio Clip ----------")]
    public AudioClip backGround;
    public AudioClip pickUpCoin;
    public AudioClip run;
    public AudioClip atk_Sword;
    public AudioClip dash;
    public AudioClip jump;
    public AudioClip jump_land;
    public AudioClip death;
    public AudioClip bound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        sfx.PlayOneShot(clip);
    }

    public void StopAudio()
    {
        sfx.Stop();
    }
}
