using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoScroll : MonoBehaviour
{
    public GameObject[] pages;
    public GameObject[] txt;
    public GameObject nextPanel;
    private const float fadetime = 0.2f;
    private float fade = 0;
    private int move = 1;
    private int page = 0, current = 0;
    private bool close = true;

    // ------------------------------------------------------------------------------------------------------------------
    void Start()
    {
        if (pages.Length > 0)
            SetPage(0);
        Hide();
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void SetPage(int p)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == p);
        }
        current = page;
    }

    // ------------------------------------------------------------------------------------------------------------------
    void Hide()
    {
        foreach (MaskableGraphic t in GetComponentsInChildren<MaskableGraphic>())
        {
            Color c = t.color;
            t.color = new Color(c.r, c.g, c.b, 0);
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void OnMouseDown()
    {
        page++;
        close = (page >= pages.Length);
        move = -1;
    }

    // ------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        fade += move * Time.deltaTime / fadetime;

        if (move == -1 && fade <= 0)
        {

            if (close)
            {
                nextPanel.SetActive(true);
                nextPanel.SendMessage("StartGame");
                this.gameObject.SetActive(false);
            }
            else
                SetPage(page);
            fade = 0;
            move = 1;
        }
        if (move == 1 && fade >= 1)
        {
            if (close)
                close = false;
            fade = 1;
            move = 0;
        }

        if (close)
        {
            foreach (MaskableGraphic t in GetComponentsInChildren<MaskableGraphic>())
            {
                Color c = t.color;
                t.color = new Color(c.r, c.g, c.b, fade);
            }
        }
        else
        {
            foreach (MaskableGraphic t in pages[current].GetComponentsInChildren<MaskableGraphic>())
            {
                Color c = t.color;
                t.color = new Color(c.r, c.g, c.b, fade);
            }
        }
    }

    // ------------------------------------------------------------------------------------------------------------------
    private void StartGame()
    {
        Hide();

        GameObject gameState = GameObject.Find("GameState");
        GameStateManager gsm = gameState.GetComponent<GameStateManager>();

        gsm.StartGame();
    }
}