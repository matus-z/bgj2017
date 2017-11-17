using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    public Action InviteForCoffeePrefab;

    private float DayTimeRemaining;

    enum EState
    {
        SelectActor,
        SelectAction,
        SelectActedOn,
        Animation       // or time dalay
    }

    public enum EActionType
    {
        InviteForCoffee,
    }

    public Person ActorSelected { get; private set; }
    public Action ActionSelected { get; private set; }

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

        //if(ActorSelected && ActionSelected)
        //{
        //    Debug.Log("Pick person 2");
        //}
        //else if(ActorSelected)
        //{
        //    Debug.Log("Pick action");
        //}
        //else
        //{
        //    Debug.Log("Pick person 1");
        //}
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void OnGUI()
    {
        // TODO Matus : draw and add style, position ...
        GUI.Label(new Rect(2, 2, 1000, 100), "Time until the night comes : " + DayTimeRemaining);
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectActor(Person actor)
    {
        ActorSelected = actor;
        foreach (EActionType at in actor.ActionsEnabled)
        {
            switch(at)
            {
                case EActionType.InviteForCoffee:
                    Instantiate(InviteForCoffeePrefab, actor.gameObject.transform.position, Quaternion.identity);
                    break;
            }

            Debug.Log(at);
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectAction(Action action)
    {
        ActionSelected = action;
        Debug.Log(action);
    }
}
