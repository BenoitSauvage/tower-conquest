using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Unit {

    private void Start() {
        maxLife = GV.SOLDIER_MAX_LIFE;
        movingRange = GV.SOLDIER_MOVING_RANGE;

        life = maxLife;
        player = GameManager.Instance.GetCurrentPlayer();

        GetComponent<Renderer>().material = Material.Instantiate(
            Resources.Load<Material>("Materials/Soldier_" + player)
        );
	}
}
