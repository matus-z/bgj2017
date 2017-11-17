using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Action> Actions;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseDown()
    {
        Debug.Log(gameObject.name);
    }
}
