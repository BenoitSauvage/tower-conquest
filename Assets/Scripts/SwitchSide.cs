using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSide : MonoBehaviour {

    public float speed;
    float angle;

    bool turn;
    public GameObject player_1;
    public GameObject player_2;
    public List<string> player = new List<string>{ "player_1", "player_2" };

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

            angle = transform.rotation.eulerAngles.y;
            if (angle >= 0 && angle < 180)
            {
                transform.Rotate(0, Time.deltaTime * speed, 0);
            }
            if (angle >= (179))
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                player_1.gameObject.SetActive(false);
                player_2.gameObject.SetActive(true);
                turn = false;
            }
        }
        //------------------------------------------------------
        if (!turn) {
            angle = transform.rotation.eulerAngles.y;
            if (angle >= 180 && angle < 360)
            {
                transform.Rotate(0, Time.deltaTime * speed, 0);
            }
            if (angle >= (355))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                player_1.gameObject.SetActive(true);
                player_2.gameObject.SetActive(false);
                turn = true;
            }
        }
        //------------------------------------------------------
    }

    /*
    void SwitchPlayer(float angleToRotate) {
        angle = transform.rotation.eulerAngles.y;
        if (angle < angleToRotate)
        {
            transform.Rotate(angleToRotate - 180, Time.deltaTime * speed, 0);
            Debug.Log("test1");
        }
        if (angle >= (angleToRotate))
        {
            transform.eulerAngles = new Vector3(0, angleToRotate, 0);
            Debug.Log("test2");
            Debug.Log(angle = transform.rotation.eulerAngles.y);
        }

        Debug.Log(angle = transform.rotation.eulerAngles.y);
    }
    */
}
