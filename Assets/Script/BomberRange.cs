using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberRange : MonoBehaviour
{
    // Start is called before the first frame update [SerializeField] private Enemy parent; 
   [SerializeField] private Bomber parent; 

    void Start()
    {
       // parent = GetComponentInChildren<Enemy>();
    }

    void Update(){
        transform.position = parent.transform.position;
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
