using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject bullet;
    public float bullet_speed, damage;
    public float SHOOTING_DELAY = 0.3f;
    private float shooting_timer;
    void Start()
    {
        shooting_timer = SHOOTING_DELAY;
    }
    
    void FixedUpdate()
    {
        if (Input.GetAxis("Triggers") > 0f)
        if(shooting_timer > 0)
        {
            shooting_timer -= Time.deltaTime;
        }
        else
        {
            Shoot();
            shooting_timer = SHOOTING_DELAY;
        }
    }

    void Shoot()
    {
        GameObject bul = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D bul_rb = bul.GetComponent<Rigidbody2D>();
        bul_rb.velocity = transform.up * bullet_speed * Time.deltaTime;
    }
}
