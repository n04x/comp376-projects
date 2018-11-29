using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BlackJackAffected : MonoBehaviour
{
    // Start is called before the first frame update
    public struct CardSuit {public bool heart,spade,club,diamond;};


    protected int current_mode;
    protected int target_mode;
    protected CardSuit suit;
    [SerializeField]protected BlackJackController blackJack;
    //protected abstract void changeMode(int new_mode);//if mode= x, then change stats accordingly

    protected  void updateTargetMode(){

        //if a suit is x value, it will be in y target mode depending on range
        // 0 = hearts, 1 = diamonds, 2 = clubs, 3 = spades.
         // private int hearts;
        // private int diamonds;
        // private int clubs;
        // private int spades;
        if(suit.heart) target_mode = blackJack.kind[0];
        if(suit.diamond) target_mode = blackJack.kind[1];
        if(suit.club) target_mode = blackJack.kind[2];
        if(suit.spade) target_mode = blackJack.kind[3];
    }
    protected virtual void updateCurrentMode(){
        if(current_mode != target_mode)
        {
            current_mode = target_mode;
        }
    }
    

}
