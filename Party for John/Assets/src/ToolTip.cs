using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
	public bool On;
	public float onX;
	public float offX;
	private float targetX;

    // ------------------------------------------------------------------------------------------------------------------
    private void Start ()
    {
        onX = transform.position.x;
		offX = 10f;
		targetX = offX;
	}

    // ------------------------------------------------------------------------------------------------------------------
    private void Update ()
    {
		targetX = On ? onX : offX;
		float smoothTime = 0.01F;
		float yVelocity = 0.0F;
		float newPosition = Mathf.SmoothDamp(transform.position.x, targetX, ref yVelocity, smoothTime);
		transform.position = new Vector3(newPosition,transform.position.y, 0);

	    transform.localScale = (transform.position.x == offX)
            ? Vector3.zero
			: transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
	}
}
