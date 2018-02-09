using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SimulatorManager : MonoBehaviour {

    public GameObject regionPrefab;
    public GameObject textObject;
    private Text statusText;
    public bool migrating = false;
    private bool initialized = false;
    private int counter = 0;

    void Start () {
        statusText = textObject.GetComponent<Text>();
	}


	
	// Update is called once per frame
	void Update () {

        if (!initialized) {
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log("pressed");
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                GameObject region = Instantiate(regionPrefab, pos, Quaternion.identity);
            }

            if (Input.GetKeyDown(KeyCode.I)) {
                foreach (Region r in FindObjectsOfType<Region>()) {
                    r.GetReady();
                }
                initialized = true;
            }

        } else {
            if (Input.GetKeyDown(KeyCode.Space)) {
                migrating = !migrating;
                statusText.text = migrating ? "Migrating" : "Paused";
            }
        }
    }

    private void FixedUpdate() {

        if (migrating) {
            counter++;
            if (counter % 30 == 0) {
                foreach (Region r in FindObjectsOfType<Region>()) {
                    r.Migrate();
                }
            }
        }
    }
}
