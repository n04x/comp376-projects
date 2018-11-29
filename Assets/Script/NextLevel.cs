using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    public static int currentLevel = 1;

  


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("INSIDE");

        if (other.gameObject.tag == "Player")
        {
            NextLevel.currentLevel++;
            Debug.Log("currentLevel: " + NextLevel.currentLevel);
            Application.LoadLevel(1);
        }
    }
}
