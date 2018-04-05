using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text winnerText;

	// Use this for initialization
	void Start () {
        winnerText.text = "Player " + GameManager.Instance.GetWinner() + " win";
	}
}
