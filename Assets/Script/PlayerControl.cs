
using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float movementSpeed = 2f;
	 Rigidbody2D rb;

  
    public float dashValue = 1.5f;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update(){


	}
	void FixedUpdate () {
		ProcessInput ();
	}

	void ProcessInput(){

        //Below are left joystick controls
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 targetVelocity = new Vector2(horizontal, vertical);

        if (horizontal != 0 || vertical != 0) { 
        if (targetVelocity.magnitude >= 1)
            targetVelocity = targetVelocity.normalized;
         targetVelocity = targetVelocity * movementSpeed * Time.deltaTime;
        rb.velocity = targetVelocity;
        }
        Debug.Log(rb.velocity.magnitude);

        //Right Stick Controls
        float right_horizontal = Input.GetAxis("RHorizontal");
        float right_vertical = Input.GetAxis("RVertical");
        if (horizontal != 0 || vertical != 0)
        {
            transform.up = (new Vector2(right_horizontal, right_vertical));
        }

        //Dash
        if(Input.GetButtonDown("LB"))
        {
           
                rb.AddForce(targetVelocity*dashValue*movementSpeed*Time.deltaTime, ForceMode2D.Impulse);

        }

    }


}
