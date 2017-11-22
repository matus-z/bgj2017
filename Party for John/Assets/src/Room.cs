using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Room : MonoBehaviour
{
    public enum ERoomState
    {
        Clean = 0,
        Phone = 1,
        Pc = 2,
        HeadGear = 3,
    };

    public ERoomState RoomState = ERoomState.HeadGear;

    public int Row;
    public int Col;

	public Sprite[] Bcg;
	public Sprite[] Frg;
	private SpriteRenderer bcgrender;
	private SpriteRenderer frgrender;
	private SpriteRenderer transrender;
	private float trans = 1.0f;//transition effect
	public float transTime = 1.0f; 

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        RoomState = ERoomState.HeadGear;
		bcgrender = GetComponentsInChildren<SpriteRenderer> ()[1];
		frgrender = GetComponentsInChildren<SpriteRenderer> ()[2];
		transrender = GetComponentsInChildren<SpriteRenderer> ()[3];
        RedrawSprite();
    }

    // ------------------------------------------------------------------------------------------------------------------
	public void Rotate(int angle)
    {
        frgrender = GetComponentsInChildren<SpriteRenderer> () [2];
		frgrender.transform.Rotate (new Vector3 (0, 0, angle));
	}

    // ------------------------------------------------------------------------------------------------------------------
    void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();

        gsm.SelectRoom(this);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ChangeRoomState(int change)
    {
        int rsNum = (int) RoomState + change;
        if (rsNum < 0) rsNum = 0;
        if (rsNum > 3) rsNum = 3;
		trans = 1.0f;
        RoomState = (ERoomState) rsNum;
        RedrawSprite();
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    private void RedrawSprite()
    {
		bcgrender.sprite = Bcg[(int)RoomState];
		frgrender.sprite = Frg [(int)RoomState];
    }

	private void FixedUpdate(){

		if (trans > 0.0f) {
			trans -= Time.deltaTime / transTime;
			transrender.color = new Color (1, 1, 1, trans);
		}
		RedrawSprite ();
	}
}
