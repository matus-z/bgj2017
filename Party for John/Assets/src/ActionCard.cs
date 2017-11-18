﻿using System.Collections;
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
    
    public enum EActionType
    {
        FacebookStatus,
        PhoneCall,
        EMP,
        ScreamOfTruth,
        PersonalVisit
    };

    [Tooltip("Action type")]
    public EActionType Type;

    private float Cooltime; 
    private bool IsSelected;
	private ToolTip ttip;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        IsSelected = false;
		Cooltime = 0;
		ttip = GetComponentInChildren<ToolTip> ();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
		if (Cooltime <= 0) {
			GameObject gameState = GameObject.Find("GameState");
			GameStateManager gsm = gameState.GetComponent<GameStateManager> ();

			gsm.SelectActionCard (this);
			//ttip.On = true;
		}
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void ApplyChange(Room r, int change = 0)
    {
        r.ChangeRoomState(change);
        Cooltime = Cooldown;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SetSelected(bool selected)
    {
        IsSelected = selected;
    }

    // ------------------------------------------------------------------------------------------------------------------
	public void FixedUpdate()
    {

        if (Cooltime > 0)
			Cooltime -= Time.deltaTime;
		else
			Cooltime = 0;
		
		foreach (Image i in GetComponentsInChildren<Image>()) {
			if(i.tag == "timer")
			i.fillAmount = Cooltime / Cooldown;
			if(i.tag == "highlight")
			i.color = new Color (1, 1, 1, IsSelected ? 1 : 0);
		}
	}
}
