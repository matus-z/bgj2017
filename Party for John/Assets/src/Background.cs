using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public Sprite day;
	public Sprite night;
<<<<<<< HEAD:Party for John/Assets/src/Background.cs
	public Color c_day;
	public Color c_night;
	public SpriteRenderer back;
	// Use this for initialization
	void Start () {
		
	}

	public void SetSprite(bool isDay){
=======

    // ------------------------------------------------------------------------------------------------------------------
    public void SetSprite(bool isDay)
    {
>>>>>>> 4d2cda09d777e80928ee3d6455719514f19f4273:Party for John/Assets/src/Background.cs
		SpriteRenderer spr = GetComponent<SpriteRenderer> ();
		if (spr != null) {
			spr.sprite = isDay ? day : night;
			back.color = isDay ? c_day : c_night;
		}
	}
}
