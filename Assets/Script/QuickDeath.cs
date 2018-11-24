using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is something I made for audio prefabs.
public class QuickDeath : MonoBehaviour {
  [SerializeField]
        float timer = 1f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(this.gameObject);
	}
}
