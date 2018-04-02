using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager
{

    #region singleton
    private static UnitManager instance;

    private UnitManager() { }

    public static UnitManager Instance {
        get {
            if (instance == null)
                instance = new UnitManager();

            return instance;
        }
    }
    #endregion singleton

    private Transform selectedUnit;
    private Transform unitsParent;

    public void Init (Transform _unitsParent) {
        unitsParent = _unitsParent;
    }

    public Transform GetUnitsParent () {
        return unitsParent;
    }

    public void NextTurn() {
        selectedUnit = null;

        foreach (Transform unit in unitsParent) {
            Unit u = unit.GetComponent<Unit>();
            u.NextTurn();
        }
    }

    public void SelectUnit (Transform _unit) {
        selectedUnit = _unit;

        if ((int)selectedUnit.GetComponent<Unit>().GetPlayer() == (int)GameManager.Instance.GetCurrentPlayer()) {
            GridManager.Instance.RemoveCellView();
            selectedUnit.GetComponent<Unit>().DrawMovingCell();
        }
    }

    public void MoveUnit (Transform _destination) {
        Vector2Int oldPos = new Vector2Int((int)selectedUnit.position.x, (int)selectedUnit.position.z);
        selectedUnit.GetComponent<Unit>().Move(_destination);

        GridManager.Instance.UpdateGrid(selectedUnit, oldPos);
        GridManager.Instance.RemoveCellView();

    }

    public void DeselectUnit () {
        GridManager.Instance.RemoveCellView();
    }
}
