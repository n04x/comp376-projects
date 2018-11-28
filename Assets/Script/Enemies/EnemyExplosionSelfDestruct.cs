using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplosionSelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SelfD());  
    }

    IEnumerator SelfD() {
        yield return new WaitForSeconds(3.0f); // Wait to make sure th eanimation is over
        Destroy(gameObject); // Destroy prefab
    }
}
