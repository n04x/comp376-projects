using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowAlice : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject target;
    [SerializeField] Vector3 offset = new Vector3(0,0,-10);
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = target.transform.position + offset;
    }
}
