using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    private float DayTimeRemaining;

    enum EState
    {
        SelectActor,
        SelectAction,
        SelectActedOn,
        Animation       // or time dalay
    }

    private Person ActorSelected;
    private Action ActionSelected;

    private EState CurrentState;

    // ------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        DayTimeRemaining = DayLength;
        CurrentState = EState.Animation;
	}

    // ------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        DayTimeRemaining -= Time.deltaTime;
	}

    // ------------------------------------------------------------------------------------------------------------------
    private void OnGUI()
    {
        // TODO Matus : draw and add style, position ...
        GUI.Label(new Rect(2, 2, 1000, 100), "Time until the night comes : " + DayTimeRemaining);
    }
}
