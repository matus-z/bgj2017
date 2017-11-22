using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public Sprite day;
	public Sprite night;
public Color c_day;
	public Color c_night;
	public SpriteRenderer back;
	// Use this for initialization
	void Start () {
		
	}

	public void SetSprite(bool isDay){
SpriteRenderer spr = GetComponent<SpriteRenderer> ();
		if (spr != null) {
			spr.sprite = isDay ? day : night;
			back.color = isDay ? c_day : c_night;
		}
	}
}
