using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Culture {
    European,
    Asian
}

public enum Country: int {
    US = 1,
    Canada = 2
}

public enum Language {
    English,
    Spanish
}


public class Region : MonoBehaviour {

    public float locationX;
    public float locationY;
    public float capacity;
    public float influence;
    public float immiBarrier;
    public Dictionary<Language, float> first;
    public Dictionary<Language, float> secondary;
    public Culture culture;
    public Country country;
    public Language official;


    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
