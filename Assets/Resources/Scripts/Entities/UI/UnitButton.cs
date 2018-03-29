using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitButton : MonoBehaviour {

    public GV.UNIT_TYPE unitType;

    public void OnUnitButtonClick() {
        InputManager.Instance.isPlacingUnit = true;
        GridManager.Instance.RemovePlacingCell();
        GridManager.Instance.CreateGhostUnit(unitType);
        GridManager.Instance.ShowPlayerCells();
    }
}
