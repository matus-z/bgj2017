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

    public int GridPosX;
    public int GridPosY;

    public Sprite BcgClean;
    public Sprite BcgPhone;
    public Sprite BcgPc;
    public Sprite BcgHeadGear;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        RoomState = ERoomState.HeadGear;
        RedrawSprite();
    }

    // ------------------------------------------------------------------------------------------------------------------
    void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();

        gsm.SelectRoom(this);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ImproveRoomState()
    {
        int rsNum = (int) RoomState;
        if (rsNum > 0) rsNum -= 1;

        RoomState = (ERoomState) rsNum;

        RedrawSprite();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void RedrawSprite()
    {
        switch(RoomState)
        {
            case ERoomState.Clean : GetComponent<SpriteRenderer>().sprite = BcgClean; break;
            case ERoomState.Phone : GetComponent<SpriteRenderer>().sprite = BcgPhone; break;
            case ERoomState.Pc : GetComponent<SpriteRenderer>().sprite = BcgPc; break;
            case ERoomState.HeadGear : GetComponent<SpriteRenderer>().sprite = BcgHeadGear; break;
        }
    }
}
