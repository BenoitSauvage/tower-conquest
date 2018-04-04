using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : UnitButton {
    
    public override void Init() { 
        base.Init();

        cost = GV.TOWER_COINS_COST;
        unitType = GV.UNIT_TYPE.TOWER;
    }
}
