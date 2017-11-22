using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class introScroll : MonoBehaviour
{

	public string[] txt;
	public Text textfield;
	public GameObject nextPanel;
	private const float fadetime = 0.2f;
	private float fade = 0;
	private int move = 1;
	private int page = 0;
	private int count = 0;
	private bool close = true;
	// Use this for initialization
	void Start ()
	{
		if (name == "panelTitle")
			fade = 1;
		if (txt.Length > 0)textfield.text = txt [0];
	}

	private void OnMouseDown ()
	{
		page++;
		close = (page >= txt.Length);
		move = -1;
	}

	void Hide(){
		foreach (MaskableGraphic t in GetComponentsInChildren<MaskableGraphic>()) {
			Color c = t.color;
			t.color = new Color (c.r, c.g, c.b,0);
		}
	}
	// Update is called once per frame
	void Update ()
	{

		fade += move * Time.deltaTime / fadetime;

		if (move == -1 && fade <= 0) {

			if (close) {
				nextPanel.SetActive (true);
				nextPanel.SendMessage("StartGame");
				this.gameObject.SetActive (false);
			}
			else if(txt.Length>0)textfield.text = txt [page];
			fade = 0;
			move = 1;
		} 
		if (move == 1 && fade >= 1) {
			//if(close)close=false;
			fade = 1;
			move = 0;
		}

		if(txt.Length>0)textfield.color = new Color (1, 1, 1, fade);

		if (close) {
			foreach (MaskableGraphic t in GetComponentsInChildren<MaskableGraphic>()) {
				Color c = t.color;
				t.color = new Color (c.r, c.g, c.b, fade);
			}
		}
	}

	private void StartGame(){
		Hide ();
	}
}