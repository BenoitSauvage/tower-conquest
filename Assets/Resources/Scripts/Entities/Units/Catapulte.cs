using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapulte : Unit {

    public override void Init() {
        base.Init();

        maxLife = GV.CATAPULTE_MAX_LIFE;
        movingRange = GV.CATAPULTE_MOVING_RANGE;
        coinCost = GV.CATAPULTE_COINS_COST;

        attackMaxRange = GV.CATAPULTE_MAX_ATTACK_RANGE;
        attackMinRange = GV.CATAPULTE_MIN_ATTACK_RANGE;
        attackDamage = GV.CATAPULTE_DAMAGE;

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
