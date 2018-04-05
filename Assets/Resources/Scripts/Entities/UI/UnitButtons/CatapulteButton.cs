using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapulteButton : UnitButton {

    public override void Init() {
        base.Init();

        cost = GV.CATAPULTE_COINS_COST;
        unitType = GV.UNIT_TYPE.CATAPULTE;
    }
}
