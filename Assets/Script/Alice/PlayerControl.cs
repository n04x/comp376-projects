using UnityEngine;

public class PlayerControl : MonoBehaviour {
	public float movementSpeed = 2f;
	 Rigidbody2D rb;


    [SerializeField]public int current_hp,MAX_HP;
    public bool isDashing = false;
    public bool isInvincible = false;
    public bool shieldOut = false;
    public bool isSlashing = false;
    public Vector2 lStickDir;
    public Vector2 rStickDir;
    // Update is called once per frame
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        current_hp = MAX_HP;
    }
    void Update(){


	}
	void FixedUpdate () {
        rb.angularVelocity = 0;
        if (!isDashing && !isSlashing)
        {
            ProcessMovementInput();
        }

        debuggingInput();
	}
    void debuggingInput(){
        if(Input.GetKeyDown(KeyCode.Q))
        takeDamage();
       
    }

    void takeDamage(){
         HP_UI.damageHeart();
         current_hp--;
    }
	void ProcessMovementInput(){

        //Below are left joystick controls
         lStickDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (lStickDir.x != 0 || lStickDir.y != 0) { 
        if (lStickDir.magnitude >= 1)
                lStickDir = lStickDir.normalized;
            lStickDir = lStickDir * movementSpeed * Time.deltaTime;
        rb.velocity = lStickDir;
        }else
        {
            rb.velocity = Vector2.zero;
        }

        //Right Stick Controls
        rStickDir = new Vector2(Input.GetAxis("RHorizontal"), Input.GetAxis("RVertical"));
        if (Mathf.Abs(rStickDir.x) >= 0.02 || Mathf.Abs(rStickDir.y) >= 0.02)
        {
            rb.rotation = Mathf.Rad2Deg * Mathf.Atan(-rStickDir.x/rStickDir.y);
            if (rStickDir.y < 0) // to actually rotate downwards
                rb.rotation += 180;
        }else if(Mathf.Abs(lStickDir.magnitude) >= 0.4f )
        {
            rb.rotation = Mathf.Rad2Deg * Mathf.Atan(-lStickDir.x / lStickDir.y);
            if (lStickDir.y < 0) // to actually rotate downwards
                rb.rotation += 180;
           //movement without rstick
        }

       


    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.tag =="Enemy")
    {
        takeDamage();
        Destroy(other.gameObject);
    }
}
}