using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorManager : MonoBehaviour {

    GameObject regionPrefab;

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0)) {
            Vector3 pos = Input.mousePosition;
            GameObject region = Instantiate(regionPrefab, pos, Quaternion.identity); 
        }	
	}
}
