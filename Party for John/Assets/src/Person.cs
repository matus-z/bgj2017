using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Person : MonoBehaviour
{
    [Tooltip("Person Name")]
    public string Name;

    [Tooltip("Plugged level - <0,1> = <Unplugged, Plugged>. The more the worse")]
    public float PluggedLevel;

    //[Tooltip("Relationship to Player - <0,1>. The more the better")]
    //public bool TrustsMe;

    [Tooltip("Peaple who have a boost towards me")]
    public List<GameObject> LovedOnes;

    public List<GameStateManager.EActionType> ActionsEnabled;

    private bool IsActive;

    public Sprite SpriteInactive;
    public Sprite SpriteActive;

    // ------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
	}

    // ------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();
        if (!gsm.ActorSelected)
            return;

        GetComponent<SpriteRenderer>().sprite = gsm.ActorSelected == this ? SpriteActive : SpriteInactive;
    }

    // ------------------------------------------------------------------------------------------------------------------
    void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();
        gsm.SelectActor(this);
    }
}
