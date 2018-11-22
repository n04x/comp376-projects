using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackController : MonoBehaviour
{
    public CardDeck player_deck;
    public CardDeck deck;


    PlayerControl pc_alice;
    public GameObject burst_meter;
    public BurstFiller hearts;
    public BurstFiller diamonds;
    public BurstFiller clubs;
    public BurstFiller spades;
    private int blackjack_score;

    public int[] kind = {0, 0, 0, 0};
    private bool playerHit = false;

    // 0 = hearts, 1 = diamonds, 2 = clubs, 3 = spades.
    private bool over = false;
    public Text blackjack_text_score;
    public Text blackjack_timer_seconds;
    public Text blackjack_timer_decimals;
    float currentTime = 0.0f;
    float lastTime = 0.0f;
    float displayTime = 30.0f;
    int time_seconds = 30;
    int time_decimals = 0;
    public Text hearts_text;
    public Text diamonds_text;
    public Text clubs_text;
    public Text spades_text;

    private void Start() {
        GameObject alice_object = GameObject.FindWithTag("Player");
        pc_alice = alice_object.GetComponent<PlayerControl>();
        blackjack_score = 0;
        if (blackjack_score < 10) {
            blackjack_text_score.text = "0" + blackjack_score;
        } else {
            blackjack_text_score.text = blackjack_score.ToString();
        }
    }
    void Update() {
        if(blackjack_score > 21) {
            over = true;
        }
        if(Input.GetKeyDown(KeyCode.Z) || Input.GetButtonDown("Y"))  {
            Hit();
        }
        if(Input.GetKeyDown(KeyCode.X) ||Input.GetButtonDown("B")) {
            Vent();
        }

        // === Blackjack Timer
        lastTime = currentTime;
        currentTime = Time.time;
        displayTime -= currentTime - lastTime;

        UpdateTimer();

        time_seconds = Mathf.FloorToInt(displayTime);
        time_decimals = Mathf.FloorToInt(60 * (displayTime - time_seconds));
        if (time_seconds < 10) {
            blackjack_timer_seconds.text = "0" + time_seconds;
        } else {
            blackjack_timer_seconds.text = time_seconds.ToString();
        }
        if (time_decimals < 10) {
            blackjack_timer_decimals.text = "0" + time_decimals;
        } else {
            blackjack_timer_decimals.text = time_decimals.ToString();
        }
        
        BurstDisplay();
        Buffer();
    }
    void UpdateTimer(){
        if (displayTime < 0) {
            // TIMEOUT CHECK GOES HERE, USE DISPLAYTIME
            Vent();
            if(!over){
                pc_alice.takeDamage();
            }
            ResetTimer();

        }
    }

    void ResetTimer(){
        displayTime = 30;
    }
    void Vent() {
        if(over) {
            pc_alice.takeDamage();
            over = false;
        }
        while(player_deck.HasCards) {
            deck.Push(player_deck.Pop());
        }
        ResetKind();
        ResetTimer();

        blackjack_score = 0;
        if (blackjack_score < 10) {
            blackjack_text_score.text = "0" + blackjack_score;
        } else {
            blackjack_text_score.text = blackjack_score.ToString();
        }

    }
    void Hit() {
        if(over) {
            pc_alice.takeDamage();
            over = false;
            Vent();
        } else {
            playerHit = true;
            player_deck.Push(deck.Pop());
            blackjack_score = player_deck.HandValue();
            if (blackjack_score < 10) {
                blackjack_text_score.text = "0" + blackjack_score;
            } else {
                blackjack_text_score.text = blackjack_score.ToString();
            }
            ResetTimer();
        }
        ResetKind();
        player_deck.KindsCounter(kind, blackjack_score);
    }
    void Buffer() {
        BlackJack();
        hearts.fillMeter(kind);
        diamonds.fillMeter(kind);
        clubs.fillMeter(kind);
        spades.fillMeter(kind);
    }

    void BlackJack() {
        bool blackjack;
        if(blackjack_score == 21) {
            blackjack = true;
            for(int i = 0; i < kind.Length; i++) {
                kind[i] = 21;
            }
        } else {
            blackjack = false;
        }
        hearts.SetBlackJack(blackjack);
        diamonds.SetBlackJack(blackjack);
        clubs.SetBlackJack(blackjack);
        spades.SetBlackJack(blackjack);
    }

    // reset the kind array to push updated value.
    void ResetKind() {
        for(int i = 0; i < kind.Length; i++) {
            kind[i] = 0;
        }
    }
    void BurstDisplay() {
        if (kind[0] < 10) {
            hearts_text.text = "0" + kind[0];
        } else {
            hearts_text.text = kind[0].ToString();
        }

        if (kind[1] < 10) {
            diamonds_text.text = "0" + kind[1];
        } else {
            diamonds_text.text = kind[1].ToString();
        }

        if (kind[2] < 10) {
            clubs_text.text = "0" + kind[2];
        } else {
            clubs_text.text = kind[2].ToString();
        }

        if (kind[3] < 10) {
            spades_text.text = "0" + kind[3];
        } else {
            spades_text.text = kind[3].ToString();
        }
    }
}
