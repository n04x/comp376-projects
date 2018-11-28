using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : BlackJackAffected {


    [SerializeField] GameObject heart_audio_prefab;
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

    bool hurt  = false;
    float hurtTimer = 0.1f;

    [SerializeField] float HURT_INVINCIBILITY_DURATION = .25f;

    float hurt_invincibility_timer;
    private bool hurtInvincible = false;

    AudioSource hurtSound;

    // Update is called once per frame
    private void Start()
    {
        

        rb = GetComponent<Rigidbody2D>();
        //TODO Find a way to carry over health to next level
        current_hp = MAX_HP;
        Debug.Log("current_hp: " + current_hp);
        suit.heart = true;
        hurt_invincibility_timer = HURT_INVINCIBILITY_DURATION;
    }
    void Update(){
        if(current_hp > 0) {
            updateCurrentMode();
            updateTargetMode();
            updateHurt();
        } else {
            GameOver();        
        }

        hurtSound = GetComponent<AudioSource>();
    }

    void updateHurt(){
        if(hurt)
        {
            hurtTimer -= Time.deltaTime;
            hurtSound.Play();
        }
        if(hurtTimer <=0)
        {
            hurt = false;
        }

        if(hurtInvincible)
        {
            hurt_invincibility_timer -=Time.deltaTime;
            if(hurt_invincibility_timer <=0)
            {
                 hurt_invincibility_timer =HURT_INVINCIBILITY_DURATION;
                 hurtInvincible = false;
             }
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
            if (!hurt&&!isDashing && !isSlashing)
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
         takeDamage(Vector3.zero, 0);
         
    }
    public void takeDamage(Vector3 dir, float value)
    {

        takeDamage( dir,  value,1);
    }

    public void takeDamage(Vector3 dir, float value, int damage){
        if(current_hp > 0&& !isInvincible &&!hurtInvincible){
         ScreenEffects.Shake();
         for(int i = 0 ; i < damage; i ++){
         HP_UI.damageHeart();
         current_hp--;
         hurtInvincible = true;
        }
         hurt = true;
         hurtTimer = 0.1f;
         Vector2 kb_dir = new Vector2(transform.position.x - dir.x,transform.position.y - dir.y);
         rb.velocity = (kb_dir * value);

         }
    }
    public void heal(){
        if(current_hp < MAX_HP){
        if(heart_audio_prefab!=null) Instantiate(heart_audio_prefab);
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
            NextLevel.currentLevel = 1;
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