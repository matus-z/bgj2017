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

	public void SetSprite(GameStateManager.EDayNight dayOrNight)
    {
        SpriteRenderer spr = GetComponent<SpriteRenderer> ();
        if (spr == null) return;

        switch(dayOrNight)
        {
            case GameStateManager.EDayNight.Day:
                spr.sprite = day;
                back.color = c_day;
                break;
            case GameStateManager.EDayNight.Night:
                spr.sprite = night;
                back.color = c_night;
                break;
        }
    }
}
