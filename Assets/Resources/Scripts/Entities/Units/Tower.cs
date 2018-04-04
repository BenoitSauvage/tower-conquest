using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit {

    public override void Init() {
        base.Init();

        maxLife = GV.TOWER_MAX_LIFE;
        movingRange = GV.TOWER_MOVING_RANGE;
        coinCost = GV.TOWER_COINS_COST;

        attackMaxRange = GV.TOWER_MAX_ATTACK_RANGE;
        attackMinRange = GV.TOWER_MIN_ATTACK_RANGE;
        attackDamage = GV.TOWER_DAMAGE;

        life = maxLife;
        moves = movingRange;
    }
}
