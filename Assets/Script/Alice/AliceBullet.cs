using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceBullet : AliceWeapon
{
    // Start is called before the first frame update

    //public int damage = 1;
    

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
            //TODO interaction depending on object
            //destroy bullet
            Destroy(gameObject);
            
    }
}
