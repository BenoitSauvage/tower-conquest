using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totaly_New_UnitButton : UnitButton {

    public override void Init()
    {
        base.Init();

        cost = GV.TOTALY_NEW_UNIT_COINS_COST;
        unitType = GV.UNIT_TYPE.TOTALY_NEW_UNIT;
    }
}
