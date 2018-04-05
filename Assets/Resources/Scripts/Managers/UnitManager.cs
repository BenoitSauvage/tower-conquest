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
            switch (InputManager.Instance.GetActionType()) {
                case GV.ACTION_TYPE.ATTACK:
                    GridManager.Instance.RemoveAttackView();
                    selectedUnit.GetComponent<Unit>().DrawAttackCell();
                    break;
                case GV.ACTION_TYPE.MOVE:
                    GridManager.Instance.RemoveCellView();
                    selectedUnit.GetComponent<Unit>().DrawMovingCell();
                    break;
            }
        }
    }

    public void MoveOrAttackUnit (Transform _target) {
        switch (InputManager.Instance.GetActionType()) {
            case GV.ACTION_TYPE.ATTACK:
                selectedUnit.GetComponent<Unit>().Attack(_target);
                GridManager.Instance.RemoveAttackView();
                break;
            case GV.ACTION_TYPE.MOVE:
                Vector2Int oldPos = new Vector2Int((int)selectedUnit.position.x, (int)selectedUnit.position.z);
                selectedUnit.GetComponent<Unit>().Move(_target);

                GridManager.Instance.UpdateGrid(selectedUnit, oldPos);
                GridManager.Instance.RemoveCellView();
                break;           
        }
    }

    public void DeselectUnit () {
        GridManager.Instance.RemoveCellView();
        GridManager.Instance.RemoveAttackView();
    }

    public void KillUnit (Transform _unit) {
        foreach (Transform child in _unit) {
            if (child.CompareTag(GV.GENERIC_UNIT_TAG))
                child.GetComponent<Renderer>().enabled = false;

            if (child.CompareTag(GV.UNIT_TAG_SOLDIER))
                foreach (Transform grand_child in child)
                    grand_child.GetComponent<Renderer>().enabled = false;
        }
            

        Transform particles = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/UnitExplosion")).transform;
        particles.SetParent(_unit);
        particles.localPosition = new Vector3();
    }
}
