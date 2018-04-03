using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {

    public GameObject soldier;
    public GameObject game;

    GameObject[] soldiertest;
    Vector3 pos;
    int nubSoldier;

    List<Vector3> soldiers = new List<Vector3> {
        new Vector3(0,0,0),
        new Vector3(-1,0,0),
        new Vector3(1,0,0),
        new Vector3(0,0,1),
        new Vector3(-1,0,-1),
        new Vector3(1,0,-1),
        new Vector3(-1,0,1),
        new Vector3(1,0,1),
        new Vector3(0,0,0),
    };

    // Use this for initialization
    void Start () {
        
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Keypad1)) {
            nubSoldier = 1;
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            nubSoldier = 2;
        }
        if (Input.GetKey(KeyCode.Keypad3))
        {
            nubSoldier = 3;
        }
        if (Input.GetKey(KeyCode.Keypad4))
        {
            nubSoldier = 4;
        }
        if (Input.GetKey(KeyCode.Keypad5))
        {
            nubSoldier = 5;
        }

        if (nubSoldier == 0) {
            
             
        }

        if (Input.GetKey(KeyCode.Keypad0))
        {
           inst();

           

        }
       


    }

     void inst()
    {
        for (int i = 0; i <= 5; i++)
        {
            pos = game.transform.position + soldiers[i];
            soldiertest[i] = Instantiate(soldier, pos, Quaternion.identity);
            soldiertest[i].transform.parent = game.transform;
        }
    }
}
