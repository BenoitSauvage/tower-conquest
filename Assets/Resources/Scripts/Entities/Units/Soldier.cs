using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit {

    public override void Init() {
        base.Init();

        maxLife = GV.SOLDIER_MAX_LIFE;
        movingRange = GV.SOLDIER_MOVING_RANGE;
        coinCost = GV.SOLDIER_COINS_COST;

        life = maxLife;
    }

    private void Start() {
        foreach (Transform child in transform) {
            child.GetComponent<Renderer>().material = Material.Instantiate(
                Resources.Load<Material>("Materials/Player_" + GameManager.Instance.GetCurrentPlayer())
            );
        }
	}
}
