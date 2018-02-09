using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatorManager : MonoBehaviour {

    public GameObject regionPrefab;

    void Start () {
		
	}

	public void UnpauseRegions() {
		foreach (Region r in FindObjectsOfType<Region>()) {
			r.Unpause ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetMouseButtonDown(0)) {
			Debug.Log ("pressed");
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			pos.z = 0;
            GameObject region = Instantiate(regionPrefab, pos, Quaternion.identity); 
        }	
	}
}
