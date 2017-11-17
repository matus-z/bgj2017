using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Action : MonoBehaviour
{
    public string Name;

    public string Description;

    public float PluggedCost;

    public float TimeToComplete;

    // ------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
    }

    // ------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
    }

    // ------------------------------------------------------------------------------------------------------------------
    void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();
        gsm.SelectAction(this);
    }
}
