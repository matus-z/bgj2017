using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ActionCard : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();

        gsm.SelectActionCard(this);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ApplyAction(Room r)
    {
        r.ImproveRoomState();
    }
}
