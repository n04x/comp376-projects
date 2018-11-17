using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When player is in the range of the enemy 
public class Range : MonoBehaviour {

    private Enemy parent; 

    void Start()
    {
        parent = GetComponentInParent<Enemy>();
    }

	private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            parent.Target = col.transform;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            parent.Target = col.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        parent.Target = null;
    }
}
