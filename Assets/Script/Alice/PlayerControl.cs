using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : BlackJackAffected {
	public float movementSpeed = 2f;
	 Rigidbody2D rb;


    [SerializeField]public int current_hp,MAX_HP;
    public bool isDashing = false;
    public bool isInvincible = false;
    public bool shieldOut = false;
    public bool isSlashing = false;
    public Vector2 lStickDir;
    public Vector2 rStickDir;
    public Text GameOverText;
    public Image myPanel;
    private float restartDelay = 5.0f;
    private float restartTimer;

    // Update is called once per frame
    private void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        current_hp = MAX_HP;
        suit.heart = true;
    }
    void Update(){
        if(current_hp > 0) {
            updateCurrentMode();
            updateTargetMode();
        } else {
            GameOver();        
        }
	}
    protected override void updateCurrentMode(){
        if(current_mode != target_mode)
        {
            int heartDifference = target_mode - current_mode;
            if( heartDifference> 0)
            {//heal up

                int healvalue = (int)(heartDifference*MAX_HP/21f);
                if(healvalue !=0) {
                for(int i = 0; i < healvalue ; i++)
                {
                    heal();
                }
                
                }
            }
            current_mode = target_mode;

        }
    }
	void FixedUpdate () {
        if(current_hp > 0) {
            rb.angularVelocity = 0;
            if (!isDashing && !isSlashing)
            {
                ProcessMovementInput();
            }
            debuggingInput();
        }
	}
    void debuggingInput(){
        if(Input.GetKeyDown(KeyCode.Q))
        takeDamage();
       
    }

     public void takeDamage(){

         if(current_hp > 0&& !isInvincible){
         HP_UI.damageHeart();
         current_hp--;}
    }

    public void heal(){
        if(current_hp < MAX_HP){
         HP_UI.healHeart();
         current_hp++;
        }
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

    void GameOver() {
            GameOverText.text = "GAME OVER!";
            restartTimer += Time.deltaTime;
            Color c = myPanel.color;
            while(c.a <= 255) {
                c.a += Time.deltaTime / 2;
            }
            if(restartTimer >= restartDelay) {
                Application.LoadLevel(0);
            }
    }
    public int getHP() {
        return current_hp;
    }
}