using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class WinLose : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
		GameObject gameState = GameObject.Find ("GameState");
		GameStateManager gsm = gameState.GetComponent<GameStateManager> ();
		gsm.StartGame ();
		this.gameObject.SetActive (false);
    }
}
