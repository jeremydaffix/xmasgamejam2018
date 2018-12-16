using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SoundSystem : MonoBehaviour
{

    public AudioSource sourceSound; // sons

	public AudioClip SoundGood, SoundBad, SoundVictory, SoundShake;
    
    public static SoundSystem inst;




    void Start()
    {
        inst = this;

    }


    void FixedUpdate()
    {
		


    }

    

    public void PlayGood()
    {
		sourceSound.PlayOneShot(SoundGood);
    }

    public void PlayBad()
    {
        sourceSound.PlayOneShot(SoundBad);
    }

    public void PlayVictory()
    {
        sourceSound.PlayOneShot(SoundVictory);
    }

    public void PlayShake()
    {
        sourceSound.PlayOneShot(SoundShake);
    }

}

