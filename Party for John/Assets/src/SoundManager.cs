using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{

	public AudioClip click;
	public AudioClip error;
	public AudioClip emp;
	public AudioClip doorbell;
	public AudioClip phone;
	public AudioClip keyboard;
	public AudioClip scream;

	private AudioSource audio;
	private Dictionary<string, AudioClip> soundBank = new Dictionary<string, AudioClip>();

	// Use this for initialization
	void Start () {
		this.audio = GetComponents<AudioSource> ()[1];
		this.soundBank.Add ("click", click);
		this.soundBank.Add ("error", error);
		this.soundBank.Add ("emp", emp);
		this.soundBank.Add ("doorbell", doorbell);
		this.soundBank.Add ("phone", phone);
		this.soundBank.Add ("keyboard", keyboard);
		this.soundBank.Add ("scream", scream);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlaySound(string sound) {
		this.audio.PlayOneShot (soundBank[sound]);
	}
}
