using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBullet : MonoBehaviour
{
    // Start is called before the first frame update

    int damageValue = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
            //TODO interaction depending on object
            Destroy(gameObject);
            
    }
}
