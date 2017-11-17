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
	private int rotation;
	private SpriteRenderer bcgrender;
	private SpriteRenderer frgrender;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        RoomState = ERoomState.HeadGear;
		bcgrender = GetComponentsInChildren<SpriteRenderer> ()[1];
		frgrender = GetComponentsInChildren<SpriteRenderer> () [2];
        RedrawSprite();
    }

	public void Rotate(int angle){

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
    public bool ImproveRoomState()
    {
        if (RoomState == ERoomState.Clean) return false;

        int rsNum = (int) RoomState - 1;
        RoomState = (ERoomState) rsNum;

        RedrawSprite();

        return true;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public bool DarkenRoomState()
    {
        int rsNum = (int) RoomState;
        if (rsNum >= 3) return false;

        rsNum += 1;

        RoomState = (ERoomState) rsNum;

        RedrawSprite();

        return true;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void RedrawSprite()
    {
		bcgrender.sprite = Bcg[(int)RoomState];
		frgrender.sprite = Frg [(int)RoomState];

    }
}
