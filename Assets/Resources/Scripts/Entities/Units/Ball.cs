using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Unit
{

    public override void Init()
    {
        base.Init();

        maxLife = GV.BALL_MAX_LIFE;
        movingRange = GV.BALL_MOVING_RANGE;
        coinCost = GV.BALL_COINS_COST;

        attackMaxRange = GV.BALL_MAX_ATTACK_RANGE;
        attackMinRange = GV.BALL_MIN_ATTACK_RANGE;
        attackDamage = GV.BALL_DAMAGE;

        life = maxLife;
        moves = movingRange;
    }

    public override void NextTurn()
    {
        base.NextTurn();
    }

}
