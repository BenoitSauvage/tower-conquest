using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    #region singleton
    private static InputManager instance;

    private InputManager() { }

    public static InputManager Instance {
        get {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }
    #endregion singleton

    public bool isPlacingUnit = false;

    private bool hasClicked;
    private GV.ACTION_TYPE actionType = GV.ACTION_TYPE.MOVE;

    public void Update (float _dt) {
        if (!isPlacingUnit && Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);

            if (hit.collider && (hit.collider.CompareTag(GV.UNIT_TAG_SOLDIER) || hit.collider.CompareTag(GV.UNIT_TAG_CATAPULTE))) {
                hasClicked = true;
                UnitManager.Instance.SelectUnit(hit.collider.transform);
            } else if (hasClicked && hit.collider && hit.collider.CompareTag(GV.CELL_MOVING_TAG)) {
                UnitManager.Instance.MoveUnit(hit.collider.transform);
            } else {
                hasClicked = false;
                UnitManager.Instance.DeselectUnit();
            }
        }

        if (isPlacingUnit) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);

            if (hit.collider && hit.collider.CompareTag(GV.CELL_PLACING_TAG)) {
                GridManager.Instance.ShowUnitGhost(hit.point);
            }

            if (Input.GetMouseButtonDown(0)) {
                if (hit.collider && (hit.collider.CompareTag(GV.CELL_PLACING_TAG) || hit.collider.CompareTag(GV.GHOST_UNIT_TAG)))
                    GridManager.Instance.PlaceUnit();
                else
                    GridManager.Instance.RemovePlacingCell();
                
                isPlacingUnit = false;
            }
        }
    }

    public void NextTurn () {
        isPlacingUnit = false;
        hasClicked = false;

        actionType = GV.ACTION_TYPE.MOVE;
    }

    public GV.ACTION_TYPE GetActionType() {
        return actionType;
    }

    public void UpdateActionType (GV.ACTION_TYPE _type) {
        actionType = _type;
        UIManager.Instance.UpdateActionButtons();
    }
}
