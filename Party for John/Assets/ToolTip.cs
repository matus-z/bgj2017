using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour {

	public bool On { get; set;}
	public float onX = -4f;
	public float offX;
	private float targetX;

	// Use this for initialization
	void Start () {
		offX = transform.position.x;
		targetX = offX;
	}
	
	// Update is called once per frame
	void Update () {

		targetX = On ? onX : offX;
		float smoothTime = 0.3F;
		float yVelocity = 0.0F;
		float newPosition = Mathf.SmoothDamp(transform.position.x, targetX, ref yVelocity, smoothTime);
		transform.position = new Vector3(newPosition,transform.position.y,0);

	}
}
