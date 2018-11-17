using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_UI : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerControl alice;
    [SerializeField] GameObject heart_object;
    [SerializeField]static int heartcount;
    static Stack hearts = new Stack();
    static Stack empty_containers = new Stack();
    [SerializeField] int heart_distance;
    void Start()
    {
        alice = FindObjectOfType<PlayerControl>();
        heartcount = alice.MAX_HP/2;

        for(int i = 0; i < heartcount; i++){
            GameObject haato = Instantiate(heart_object, transform.position + new Vector3(heart_distance,0,0)*i,Quaternion.identity);
            haato.transform.parent = this.transform;
            hearts.Push(haato);
            
        }

    }

    public static void damageHeart(){
      
        if(((GameObject)hearts.Peek()).transform.Find("half_heart_right").GetComponent<Image>().isActiveAndEnabled)
            ((GameObject)hearts.Peek()).transform.Find("half_heart_right").GetComponent<Image>().enabled =false;
        else if(((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().isActiveAndEnabled){
            
            ((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().enabled =false;
                    if(hearts.Count >1)
                    empty_containers.Push(hearts.Pop());
        }
 
        
    }

    public static void healHeart(){
      
       if(((GameObject)hearts.Peek()).transform.Find("half_heart_right").GetComponent<Image>().isActiveAndEnabled){
            hearts.Push(empty_containers.Pop());
            ((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().enabled =true;
                  
        }else if(((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().isActiveAndEnabled){
                        ((GameObject)hearts.Peek()).transform.Find("half_heart_right").GetComponent<Image>().enabled =true;

        }else if(hearts.Count == 1 && !((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().isActiveAndEnabled ){
                ((GameObject)hearts.Peek()).transform.Find("half_heart_left").GetComponent<Image>().enabled = true;
        }

 
    }


}
