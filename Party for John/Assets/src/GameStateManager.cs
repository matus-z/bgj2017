using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [Tooltip("Length of day (turn) in sec")]
    public float DayLength;

    public Action InviteForCoffeePrefab;
    public Action TalkPrefab;

    private float DayTimeRemaining;

    public enum EActionType
    {
        InviteForCoffee,
        Talk,
    }

    public Person ActorSelected { get; private set; }
    public Action ActionSelected { get; private set; }

    private List<Action> ActionsVisible;

    // ------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
        DayTimeRemaining = DayLength;
        ActionsVisible = new List<Action>();
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
    public void SelectPerson(Person person)
    {
        if (ActorSelected && ActionSelected)
        {
            Debug.Log("select person acted on" + Time.time);
            SelectPersonActedOn(person);
        }
        else if (!ActorSelected && !ActionSelected)
        {
            Debug.Log("select person actor" + Time.time);
            SelectPersonActor(person);
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void SelectPersonActor(Person person)
    {
        ActorSelected = person;
        float xPos = 0;

        foreach (EActionType at in person.ActionsEnabled)
        {
            Vector3 pos = person.gameObject.transform.position + Vector3.right.normalized * xPos;
            xPos += person.gameObject.transform.GetComponent<Renderer>().bounds.size.x / 2.0f;
            switch (at)
            {
                case EActionType.InviteForCoffee:
                    ActionsVisible.Add(Instantiate(InviteForCoffeePrefab, pos, Quaternion.identity));
                    break;
                case EActionType.Talk:
                    ActionsVisible.Add(Instantiate(TalkPrefab, pos, Quaternion.identity));
                    break;
            }
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void SelectPersonActedOn(Person person)
    {
        if (!ActionSelected) return;
        if (!ActorSelected) return;
        if (!person) return;

        Person ActedOn = person;

        // TODO Apply action

        ActorSelected.PluggedLevel += ActionSelected.PluggedCost;
        //ActedOn.PluggedLevel ActionSelected.

        // Clear all actions
        foreach (Action a in ActionsVisible) Destroy(a.gameObject);
        ActionsVisible.Clear();

        ActorSelected = null;
    }

    // ------------------------------------------------------------------------------------------------------------------
    public void SelectAction(Action action)
    {
        ActionSelected = action;
        //Debug.Log(action);
    }
}
