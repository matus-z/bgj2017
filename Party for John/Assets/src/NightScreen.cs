using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightScreen : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();

        gsm.EndNight();
    }
}
