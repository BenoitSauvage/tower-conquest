using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSide : MonoBehaviour {

    public float speed;
    float angle;

    bool turn;
    public GameObject player_1;
    public GameObject player_2;

    // Use this for initialization
    void Start () {
        speed = 75;
        angle = transform.rotation.eulerAngles.y;

        turn = true;
        player_1.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        //------------------------------------------------------
        if (turn) {
            rotation(180, false, true);


        }
        //------------------------------------------------------
        if (!turn) {
            rotation(360, true, false);
        }
        //------------------------------------------------------
    }

    void rotation(float angleRotation, bool one, bool two)
    {
        angle = transform.rotation.eulerAngles.y;
        if (angle >= (angleRotation - 180) && angle < angleRotation)
        {
            transform.Rotate(0, Time.deltaTime * speed, 0);
        }
        if (angle >= (angleRotation - 5))
        {
            transform.eulerAngles = new Vector3(0, angleRotation, 0);
            player_1.gameObject.SetActive(one);
            player_2.gameObject.SetActive(two);
            turn = true;
        }
    }

}
