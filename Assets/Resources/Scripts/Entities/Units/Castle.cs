using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : Unit {

    [Range(1, 2)]
    public int playerNumber;

    private void Start() {
        foreach (Transform child in transform) {
            if (child.CompareTag(GV.GENERIC_UNIT_TAG))
                child.GetComponent<Renderer>().material = Material.Instantiate(
                    Resources.Load<Material>("Materials/Player_" + playerNumber)
                );
            if (child.CompareTag(GV.LIFE_BAR_TAG))
                lifeBar = child;
        }
    }

    public override void Init() {
        base.Init();

        unitType = GV.UNIT_TYPE.CASTLE;

        maxLife = GV.CASTLE_MAX_LIFE;
        movingRange = 0;
        coinCost = 0f;

        attackMaxRange = 0;
        attackMinRange = 0;
        attackDamage = 0f;

        life = maxLife;
        player = playerNumber;
    }
}
