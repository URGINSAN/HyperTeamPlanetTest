using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    private AudioSource sfx;

    public AudioClip Lose;
    public AudioClip Collect;
    public AudioClip Win;
    public AudioClip Click;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        sfx = GetComponent<AudioSource>();
    }

    public void PlayAudio(string type)
    {
        switch (type)
        {
            case "lose":
                sfx.volume = 0.25f;
                sfx.PlayOneShot(Lose);
                break;
            case "collect":
                sfx.volume = 0.5f;
                sfx.PlayOneShot(Collect);
                break;
            case "win":
                sfx.volume = 0.5f;
                sfx.PlayOneShot(Win);
                break;
            case "click":
                sfx.volume = 0.75f;
                sfx.PlayOneShot(Click);
                break;
        }
    }
}
