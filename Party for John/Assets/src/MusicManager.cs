using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
	public AudioClip music;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
		AudioSource audio = GetComponents<AudioSource> ()[0];
		audio.clip = music;
		audio.Play ();
	}
}
