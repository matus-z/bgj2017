using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour {

	public AudioClip music;

	// Use this for initialization
	void Start () {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.clip = music;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
