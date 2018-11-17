using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeckView : MonoBehaviour
{
    CardDeck deck;
    Dictionary<int, GameObject> fetched_cards;
    int last_count;
    public Vector3 start;
    public float card_offset;
    public bool face_up = false;
    public bool reverse_layer_order = false;
    public GameObject card_prefab;


    void Start()
    {
        fetched_cards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardDeck>();
        ShowCard();
        last_count = deck.CardCount;

        deck.cardRemoved += deck_CardRemoved;
    }

    // Update is called once per frame
    void Update()
    {
        if(last_count != deck.CardCount) {
            last_count = deck.CardCount;
            ShowCard();
        }
    }

    void deck_CardRemoved(object sender, CardEventArgs e)
    {
        if (fetched_cards.ContainsKey(e.CardIndex))
        {
            Destroy(fetched_cards[e.CardIndex]);
            fetched_cards.Remove(e.CardIndex);
        }
    }

    void ShowCard() {
        int card_count = 0;

        if(deck.HasCards) {
            foreach(int i in deck.GetCards()) {
                float co = card_offset * card_count;
                Vector3 temp = start + new Vector3(co, 0.0f);
                AddCard(temp, i, card_count);
                card_count++;
            }
        }
    }
    
    void AddCard(Vector3 position, int card_index, int positional_index) {
        if(fetched_cards.ContainsKey(card_index)) {
            return;
        }

        GameObject card_object = (GameObject) Instantiate(card_prefab);
        card_object.transform.position = position;

        CardModel card_model = card_object.GetComponent<CardModel>();
        card_model.card_index = card_index;
        card_model.ToggleFace(face_up);

        SpriteRenderer sprite_renderer = card_object.GetComponent<SpriteRenderer>();
        if(reverse_layer_order) {
            sprite_renderer.sortingOrder = 51 - positional_index;
        } else {
            sprite_renderer.sortingOrder = positional_index;
        }

        fetched_cards.Add(card_index, card_object);

    }
}
