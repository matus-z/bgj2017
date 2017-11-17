using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ActionCard : MonoBehaviour
{
    public Sprite BcgSelected;
    public Sprite BcgUnselected;

    [Tooltip("Cooldown in sec")]
    public float Cooldown;

    private bool IsSelected;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        IsSelected = false;

        RedrawSprite();
    }

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

    // ------------------------------------------------------------------------------------------------------------------
    private void RedrawSprite()
    {
        GetComponent<SpriteRenderer>().sprite = IsSelected
            ? BcgSelected
            : BcgUnselected;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        RedrawSprite();
    }
}
