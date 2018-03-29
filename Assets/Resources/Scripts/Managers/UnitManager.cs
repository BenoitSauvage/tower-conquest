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

    public void SelectUnit (Transform _unit) {
        selectedUnit = _unit;

        GridManager.Instance.RemoveCellView();

        switch (selectedUnit.tag) {
            case "UnitSoldier":
                selectedUnit.GetComponent<Soldier>().DrawMovingCell();
                break;
            case "UnitCatapulte":
                break;
            case "UnitTower":
                break;
        }
    }

    public void MoveUnit (Transform _destination) {
        Vector2Int oldPos = new Vector2Int((int)selectedUnit.position.x, (int)selectedUnit.position.z);

        selectedUnit.position = new Vector3(_destination.position.x, selectedUnit.position.y, _destination.position.z);

        GridManager.Instance.UpdateGrid(selectedUnit, oldPos);
        GridManager.Instance.RemoveCellView();
    }

    public void DeselectUnit () {
        GridManager.Instance.RemoveCellView();
    }
}
