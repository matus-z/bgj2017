using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	
	public Sprite day;
	public Sprite night;


	// Use this for initialization
	void Start () {
		
	}

	public void SetSprite(bool isDay){
		SpriteRenderer spr = GetComponent<SpriteRenderer> ();
		if (spr != null)
			spr.sprite = isDay ? day : night;
	}

	// Update is called once per frame
	void Update () {
		
	}
}
