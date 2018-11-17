using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPositioning : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector2 position ;
    
    // Update is called once per frame
    void Update(){
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x,position.y,Camera.main.nearClipPlane));    
    }
}
