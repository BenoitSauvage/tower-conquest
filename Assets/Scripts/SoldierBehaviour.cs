using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {

    public GameObject game;
    GameObject soldier;

    GameObject[] soldiertest;
    Vector3 pos;
    int nubSoldier;
   





    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Keypad1)) { nubSoldier = 1; }
        if (Input.GetKey(KeyCode.Keypad2)) { nubSoldier = 2; }
        if (Input.GetKey(KeyCode.Keypad3)) { nubSoldier = 3; }
        if (Input.GetKey(KeyCode.Keypad4)) { nubSoldier = 4; }


        if (Input.GetKey(KeyCode.Keypad0))
        {
            //inst();
            
            soldier = transform.GetChild(nubSoldier).gameObject;
            //soldier.transform.position = new Vector3(0,1,0);
            

            DestroyObject(transform.GetChild(nubSoldier).gameObject);

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
       


    }

    /*
    void inst() {
        for (int i = 0; i <= 5; i++)
        {
            pos = game.transform.position + soldiers[i];
            soldiertest[i] = Instantiate(soldier, pos, Quaternion.identity);
            soldiertest[i].transform.parent = game.transform;
        }
    }
    */
}
