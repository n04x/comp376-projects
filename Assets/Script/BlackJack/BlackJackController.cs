using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackController : MonoBehaviour
{
    public CardDeck player_deck;
    public CardDeck deck;


    PlayerControl pc_alice;
    private TrailRenderer pc_trail;
    public GameObject burst_meter;
    public BurstFiller hearts;
    public BurstFiller diamonds;
    public BurstFiller clubs;
    public BurstFiller spades;
    private int blackjack_score;

    public int[] kind = { 0, 0, 0, 0 };
    private bool playerHit = false;

    //Timer critical
    private float beepTimer = 0.0f;
    AudioSource beepSound;
    public float beepStart = 10.0f; //What second to start beep
    public float beepCriticalStart = 5.0f; //What second to start beep
    public float beepInterval = 1.0f;   //Time between beeps
    public float beepCriticalInterval = 0.5f;
    public float flashLength = 0.05f; // Length of a flash in seconds
    private Color cyan;
    private Color magenta;

    // 0 = hearts, 1 = diamonds, 2 = clubs, 3 = spades.
    private bool over = false;
    public Text blackjack_text_score;
    public Text blackjack_timer_seconds;
    public Text blackjack_timer_decimals;

    float displayTime = 30.0f;
    int time_seconds = 30;
    int time_decimals = 0;
    public Text hearts_text;
    public Text diamonds_text;
    public Text clubs_text;
    public Text spades_text;

    public GameObject hitSound, ventSound, bjSound;
    bool playBJSound;

    private void Start()
    {
        playBJSound = true;
        // Set colors
        cyan = new Color (0.0f, 0.8f, 1.0f);
        magenta = new Color (1.0f, 0.0f, 98.0f/255.0f);

        GameObject alice_object = GameObject.FindWithTag("Player");
        pc_alice = alice_object.GetComponent<PlayerControl>();
        pc_trail = alice_object.GetComponent<TrailRenderer>();
        blackjack_score = 0;
        if (blackjack_score < 10)
        {
            blackjack_text_score.text = "0" + blackjack_score;
        }
        else
        {
            blackjack_text_score.text = blackjack_score.ToString();
        }

        beepSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (blackjack_score > 21)
        {
            over = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Y"))
        {
            Hit();
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetButtonDown("B"))
        {
            Vent();
        }

        // === Blackjack Timer

        displayTime -= Time.deltaTime;
        beepTimer -= Time.deltaTime;

        UpdateTimer();
        
        time_seconds = Mathf.FloorToInt(displayTime);
        time_decimals = Mathf.FloorToInt(60 * (displayTime - time_seconds));
        if (time_seconds < 10)
        {            
            blackjack_timer_seconds.text = "0" + time_seconds;
        }
        else
        {
            blackjack_timer_seconds.text = time_seconds.ToString();
        }
        if (time_decimals < 10)
        {
            blackjack_timer_decimals.text = "0" + time_decimals;
        }
        else
        {
            blackjack_timer_decimals.text = time_decimals.ToString();
        }


        // Beep noise regular (default 10 seconds) and critical. (default 5 seconds)
        if (time_seconds < beepStart)
        {
            // Set timer to magenta
            blackjack_timer_seconds.color = magenta;
            blackjack_timer_decimals.color = magenta;

            // Check if timer critical
            if (time_seconds < beepCriticalStart) 
            {

                if (beepTimer < 0) 
                {
                    // Flash every critical interval
                    StartCoroutine(FlashTimer());
                    beepSound.Play();
                    beepTimer = beepCriticalInterval;
                }
            } 
            else if (beepTimer < 0) 
            {
                // Flash every interval
                StartCoroutine(FlashTimer());
                beepSound.Play();
                beepTimer = beepInterval;
            }

        } else {
            // Set timer to cyan
            blackjack_timer_seconds.color = cyan;
            blackjack_timer_decimals.color = cyan;
        }

        BurstDisplay();
        Buffer();
    }

    IEnumerator FlashTimer () {
        // hide timer
        blackjack_timer_seconds.enabled = false;
        blackjack_timer_decimals.enabled = false;

        yield return new WaitForSeconds(flashLength); // Wait a predetermined amount of time

        // display timer
        blackjack_timer_seconds.enabled = true;
        blackjack_timer_decimals.enabled = true;
    }

    void UpdateTimer()
    {
        if (displayTime < 0)
        {
            // TIMEOUT CHECK GOES HERE, USE DISPLAYTIME
            Vent();
            if (!over)
            {
                pc_alice.takeDamage();
            }
            ResetTimer();

        }
    }

    void ResetTimer()
    {
        displayTime = 30;
    }

    void Vent()
    {
        if (!over)
        {
            if (ventSound != null) Instantiate(ventSound);
        }

        if (over)
        {
            pc_alice.takeDamage();
            over = false;
        }
        while (player_deck.HasCards)
        {
            deck.Push(player_deck.Pop());
        }
        ResetKind();

        ResetTimer();

        blackjack_score = 0;
        if (blackjack_score < 10)
        {
            blackjack_text_score.text = "0" + blackjack_score;
        }
        else
        {
            blackjack_text_score.text = blackjack_score.ToString();
        }

        // Reset trail
        pc_trail.time = 0.5f;

    }
    void Hit()
    {
        if (!over)
        {
            if (hitSound != null) Instantiate(hitSound);
        }

        if (over)
        {
            pc_alice.takeDamage();
            over = false;
            Vent();
        }
        else
        {
            playerHit = true;
            player_deck.Push(deck.Pop());
            blackjack_score = player_deck.HandValue();

            if (blackjack_score < 10)
            {
                blackjack_text_score.text = "0" + blackjack_score;
            }
            else
            {
                blackjack_text_score.text = blackjack_score.ToString();
            }


            ResetTimer();
        }

        // Modify trail length based on proximity to 21. min: 0.5s, max: 1.0s
        int handVal = player_deck.HandValue();
        pc_trail.time = 0.5f + ((float)handVal / 21) * 0.5f;


        if (blackjack_score > 21)
        {

            over = true;
            player_deck.KindsCounter(kind, blackjack_score);

            ResetKind();
            Vent();
            return;

        }
        ResetKind();
        player_deck.KindsCounter(kind, blackjack_score);
    }
    void Buffer()
    {
        BlackJack();
        hearts.fillMeter(kind);
        diamonds.fillMeter(kind);
        clubs.fillMeter(kind);
        spades.fillMeter(kind);
    }

    void BlackJack()
    {
    
        bool blackjack;
        if (blackjack_score == 21)
        {
            blackjack = true;

            if (playBJSound = true)
            {
                if (bjSound != null) Instantiate(bjSound);
                playBJSound = false;

            }

            
            for (int i = 0; i < kind.Length; i++)
            {
                kind[i] = 21;
            }
        }
        else
        {
            blackjack = false;
            playBJSound = true;
        }
        hearts.SetBlackJack(blackjack);
        diamonds.SetBlackJack(blackjack);
        clubs.SetBlackJack(blackjack);
        spades.SetBlackJack(blackjack);
    }

    // reset the kind array to push updated value.
    void ResetKind()
    {
        for (int i = 0; i < kind.Length; i++)
        {
            kind[i] = 0;
        }
    }
    void BurstDisplay()
    {
        if (kind[0] < 10)
        {
            hearts_text.text = "0" + kind[0];
        }
        else
        {
            hearts_text.text = kind[0].ToString();
        }

        if (kind[1] < 10)
        {
            diamonds_text.text = "0" + kind[1];
        }
        else
        {
            diamonds_text.text = kind[1].ToString();
        }

        if (kind[2] < 10)
        {
            clubs_text.text = "0" + kind[2];
        }
        else
        {
            clubs_text.text = kind[2].ToString();
        }

        if (kind[3] < 10)
        {
            spades_text.text = "0" + kind[3];
        }
        else
        {
            spades_text.text = kind[3].ToString();
        }
    }
}
