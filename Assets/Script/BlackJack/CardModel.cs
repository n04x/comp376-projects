using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite[] cards;
    public Sprite face_down;

    public int card_index;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void ToggleFace(bool show_face) {

        if(show_face) {
            
            spriteRenderer.sprite = cards[card_index];
        } else {
            spriteRenderer.sprite = face_down;
        }
    }
}
