using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackJackController : MonoBehaviour
{
    public CardDeck player;
    public CardDeck deck;
   
    private int blackjack_score;

    public Text blackjack_text_score;

    private void Start() {
        blackjack_score = 0;
        blackjack_text_score.text = "Current score: " + blackjack_score;
    }
    void Update() {
        if(Input.GetKeyDown(KeyCode.Z)) {
            Hit();
        }
    }

    public void Hit() {
        player.Push(deck.Pop());
        blackjack_score = player.HandValue();
        blackjack_text_score.text = "current score: " + blackjack_score;
        if(player.HandValue() > 21) {
            // TODO: Action to do when player busted 21.
        }       
    }
}
