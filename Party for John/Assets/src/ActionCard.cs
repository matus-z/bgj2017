using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider2D))]
public class ActionCard : MonoBehaviour
{
    public Sprite BcgSelected;
    public Sprite BcgUnselected;

    [Tooltip("Cooldown in sec")]
    public float Cooldown;

	private float Cooltime; 
    private bool IsSelected;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        IsSelected = false;
		Cooltime = 0;
        RedrawSprite();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
		if (Cooltime <= 0) {
			GameObject gameState = GameObject.Find ("GameState");
			GameStateManager gsm = gameState.GetComponent<GameStateManager> ();

			gsm.SelectActionCard (this);
		}
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ApplyAction(Room r)
    {
        r.ImproveRoomState();
		Cooltime = Cooldown;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void RedrawSprite()
    {
        /*GetComponent<SpriteRenderer>().sprite = IsSelected
            ? BcgSelected
            : BcgUnselected;*/
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SetSelected(bool selected)
    {
        IsSelected = selected;
        RedrawSprite();
    }

	public void FixedUpdate(){
		if (Cooltime > 0)
			Cooltime -= Time.deltaTime;
		else
			Cooltime = 0;

		Image i = GetComponentsInChildren<Image> ()[1];
			i.fillAmount = Cooltime / Cooldown;
	}
}
