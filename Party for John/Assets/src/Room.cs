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

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        RoomState = ERoomState.HeadGear;
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
    }
}
