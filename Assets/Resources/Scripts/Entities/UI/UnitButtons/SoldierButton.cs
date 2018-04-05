using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierButton : UnitButton {

    public override void Init() {
        base.Init();

        cost = GV.SOLDIER_COINS_COST;
        unitType = GV.UNIT_TYPE.SOLDIER;
    }
}
