using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulte : Unit {

    private void Start() {
        maxLife = GV.CATAPULTE_MAX_LIFE;
        movingRange = GV.CATAPULTE_MOVING_RANGE;
        coinCost = GV.CATAPULTE_COINS_COST;

        life = maxLife;
        player = GameManager.Instance.GetCurrentPlayer();

        foreach (Transform child in transform) {
            child.GetComponent<Renderer>().material = Material.Instantiate(
               Resources.Load<Material>("Materials/Player_" + player)
           );
        }
    }
}
