using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public  class ScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    public static int Game_Score = 0;
    public static Text Score_Text;
    public static int PAWN_SCORE = 100;
    public static int ROOK_SCORE = 200;
    public static int KNIGHT_SCORE = 300;
    public static int BOSS_SCORE = 500;
    void Start()
    {
        Score_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    public static void Increment(int amount){
        Game_Score += amount;

        if(Score_Text !=null)Score_Text.text = Game_Score + " SCORE";
    }

    public static void Reset(){
        Game_Score = 0;
         if(Score_Text !=null)Score_Text.text = Game_Score + " SCORE";

    }
}
