using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DASH_UI : MonoBehaviour
{
    // Start is called before the first frame update
    DashController alice_dc;
    [SerializeField] GameObject diamond;
    static int dash_counter;
    static List<GameObject> diamonds;
    void Start()
    {
        alice_dc = FindObjectOfType<DashController>();
        diamonds = new List<GameObject>();
       for(int i = 0; i < 4; i++){
            GameObject dia = Instantiate(diamond, transform.position + new Vector3(0,-40*(4-i) + 100,0),Quaternion.Euler(new Vector3(0,0,45))) as GameObject;
            dia.transform.parent = this.transform;
            diamonds.Add(dia);
            
        }
        Refresh(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void Refresh(int c){
        
        for(int i = 0; i < c; i++)
        {
                            diamonds[i].GetComponent<Image>().enabled =true;

        }
        if(c<4){
            for(; c <4 ;c++)
            {
                diamonds[c].GetComponent<Image>().enabled =false;
            }
        }

    }
}
