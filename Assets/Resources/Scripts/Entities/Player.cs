using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private float coins;

	// Use this for initialization
	public Player() {
        coins = GV.PLAYER_STARTING_COINS;
	}

    public void UpdateCoins (float _value) {
        coins += _value;
    }

    public float GetCoins () {
        return coins;
    }
}
