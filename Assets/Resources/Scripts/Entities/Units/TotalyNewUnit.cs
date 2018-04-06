using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalyNewUnit : Unit {

    public override void Init()
    {
        base.Init();

        maxLife = GV.TOTALY_NEW_UNIT_MAX_LIFE;
        movingRange = GV.TOTALY_NEW_UNIT_MOVING_RANGE;
        coinCost = GV.TOTALY_NEW_UNIT_COINS_COST;

        attackMaxRange = GV.TOTALY_NEW_UNIT_MAX_ATTACK_RANGE;
        attackMinRange = GV.TOTALY_NEW_UNIT_MIN_ATTACK_RANGE;
        attackDamage = GV.TOTALY_NEW_UNIT_DAMAGE;

        life = maxLife;
        moves = movingRange;
    }

    public override void NextTurn()
    {
        base.NextTurn();
    }
}
