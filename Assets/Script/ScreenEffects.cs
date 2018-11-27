using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffects : MonoBehaviour
{
    // Start is called before the first frame update

    float shake_value = 0.15f;
    const float SHAKE_DURATION = 0.25f;
    float shake_timer;
    static bool is_shaking = false;

    Vector3 original_pos;

    [SerializeField]GameObject target;
    [SerializeField] Vector3 offset = new Vector3(0,0,-10);
    void Start()
    {
        original_pos = target.transform.position + offset;
        shake_timer = SHAKE_DURATION;
    }

    // Update is called once per frame
    void Update()
    {
        original_pos = target.transform.position + offset;
        if(is_shaking && shake_timer >0)
        {
            Camera.main.transform.position = new Vector3(Random.Range(original_pos.x - shake_value, original_pos.x+shake_value),Random.Range(original_pos.y - shake_value, original_pos.y+shake_value),transform.position.z);
            shake_timer -= Time.deltaTime;
        }
        else{
            transform.position = original_pos;
        }
        if(shake_timer <= 0)
        {
            is_shaking = false;
            shake_timer = SHAKE_DURATION;

            
        }

    }

    public static void Shake(){
        if(!is_shaking)
          is_shaking = true;
    }
}
