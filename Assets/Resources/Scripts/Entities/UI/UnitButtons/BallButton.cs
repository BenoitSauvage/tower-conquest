using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallButton : UnitButton
{

    public override void Init()
    {
        base.Init();

        cost = GV.BALL_COINS_COST;
        unitType = GV.UNIT_TYPE.BALL;
    }
}
