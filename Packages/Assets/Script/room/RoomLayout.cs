using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLayout : MonoBehaviour
{
    public float width;
    public float height;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(position, new Vector3(position.x + width, position.y, position.z));
        Gizmos.DrawLine(new Vector3(position.x, position.y - height, position.z), new Vector3(position.x + width, position.y - height, position.z));
        Gizmos.DrawLine(position, new Vector3(position.x, position.y - height, position.z));
        Gizmos.DrawLine(new Vector3(position.x + width, position.y, position.z), new Vector3(position.x + width, position.y - height, position.z));
    }
}
