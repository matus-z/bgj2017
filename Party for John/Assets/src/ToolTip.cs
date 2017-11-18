using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour {

	public bool On;
	public float onX;
	public float offX;
	private float targetX;

	// Use this for initialization
	void Start () {
		onX = transform.position.x;
		offX = 10f;
		targetX = offX;
	}
	
	// Update is called once per frame
	void Update () {

		targetX = On ? onX : offX;
		float smoothTime = 0.01F;
		float yVelocity = 0.0F;
		float newPosition = Mathf.SmoothDamp(transform.position.x, targetX, ref yVelocity, smoothTime);
		transform.position = new Vector3(newPosition,transform.position.y,0);
		//transform.localScale = new Vector3 (1, 1 - transform.position.x / 14, 1);
	}
}
