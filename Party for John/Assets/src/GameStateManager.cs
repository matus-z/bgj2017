using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    public GameObject DayProgressTimer;
    
    private float DayTimeRemaining;

    public ActionCard ActionCardSelected { get; private set; }

    private enum EState
    {
        SelectActionCard,
        SelectRoom
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    private void Start ()
    {
        DayTimeRemaining = DayLength;
	}

    // ------------------------------------------------------------------------------------------------------------------
    private void Update ()
    {
        DayTimeRemaining -= Time.deltaTime;
        if (DayTimeRemaining < 0)
        {
            EndDay();
            return;
        }

        if (!DayProgressTimer) return;
        Image img = DayProgressTimer.GetComponent<Image>();
        if (!img) return;

        img.fillAmount = DayTimeRemaining / DayLength;
        Debug.Log(img.fillAmount);
    }
    
    // ------------------------------------------------------------------------------------------------------------------
    public void SelectActionCard(ActionCard actionCard)
    {
        if (GetState() != EState.SelectActionCard) return;
        if (!actionCard) return;

        ActionCardSelected = actionCard;
        ActionCardSelected.SetSelected(true);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectRoom(Room room)
    {
        if (GetState() != EState.SelectRoom) return;
        if (!room) return;
        if (!ActionCardSelected) return;

        ActionCardSelected.ApplyAction(room);
        ActionCardSelected.SetSelected(false);
        ActionCardSelected = null;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private EState GetState()
    {
        return ActionCardSelected ? EState.SelectRoom : EState.SelectActionCard;
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void EndDay()
    {
        DayTimeRemaining = DayLength;

        if (ActionCardSelected) ActionCardSelected.SetSelected(false);

        ActionCardSelected = null;
    }
}
