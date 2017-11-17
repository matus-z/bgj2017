using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Room : MonoBehaviour
{
    public bool IsLocked;

    public bool IsPowered;

    private Person PersonInhabitant;

    // ------------------------------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start ()
    {
	}

    // ------------------------------------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update ()
    {
	}
}
