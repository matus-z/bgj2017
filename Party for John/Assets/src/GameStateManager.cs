using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;
    
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
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void OnGUI()
    {
        // TODO Matus : draw and add style, position ...
        GUI.Label(new Rect(2, 2, 1000, 100), "Time until the night comes : " + DayTimeRemaining);
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
}
