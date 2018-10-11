using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    List<int> cards;
    public bool isGameDeck;

    public bool HasCards {
        get { return cards != null && cards.Count > 0; }
    }

    public event CardEventHandler cardRemoved;
    public event CardEventHandler cardAdded;

    void Awake() {
        cards = new List<int>();
        if(isGameDeck) {
            CreateDeck();
        }
    }

    public int CardCount {

        get {
            if(cards == null) {
                return 0;
            } else {
                return cards.Count;
            }
        }
    }

    public IEnumerable<int> GetCards() {
        foreach(int i in cards) {
            yield return i;
        }
    }
    // Pop (remove) a card from a deck
    public int Pop() {
        int temp = cards[0];
        cards.RemoveAt(0);

        if(cardRemoved != null) {
            cardRemoved(this, new CardEventArgs(temp));
        }
        return temp;
    }

    // Push (insert) a card into a deck
    public void Push(int card) {
        cards.Add(card);
        if(cardAdded != null) {
            cardAdded(this, new CardEventArgs(card));
        }
    }

    public int HandValue() {
        // total current cards value
        int total = 0;
        // keep the number of aces to be able to choose between 1 and 11.
        int aces = 0;
        foreach(int card in GetCards()) {
            // pos 0 -> Aces
            // pos 1 -> 2
            // pos 2 -> 3
            // pos 3 -> 4
            // pos 4 -> 5
            // etc. we can see a pattern, it's pos + 1 = values except for 0
            // and pos 9, 10, 11, 12 are 10 (10, J, Q, K respectively)
            int card_rank = card % 13;

            if(card_rank > 0 && card_rank <= 9) {
                card_rank += 1;
                total += card_rank;
            } else if(card_rank > 9) {
                card_rank += 10;
                total += card_rank;
            } else {
                aces++;
            }
        }
        for(int i = 0; i < aces; i++) {
            if(total + 11 <= 21) {
                total += 11;
            } else {
                total += 1;
            }
        }

        return total;
    }

    // Build a deck.
    public void CreateDeck() {
        cards = new List<int>();

        for(int i = 0; i < 52; i++) {
            cards.Add(i);
        }

        int n = cards.Count;
        while(n > 1) {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }
}
