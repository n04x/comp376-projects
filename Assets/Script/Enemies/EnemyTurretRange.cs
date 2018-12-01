using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretRange : MonoBehaviour
{
    private EnemyTurret parent;

    void Start()
    {
        parent = GetComponentInParent<EnemyTurret>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
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
