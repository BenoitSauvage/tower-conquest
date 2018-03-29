using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

    #region singleton
    private static InputManager instance;

    private InputManager() { }

    public static InputManager Instance
    {
        get
        {
            if (instance == null)
                instance = new InputManager();

            return instance;
        }
    }
    #endregion singleton

    private bool hasClicked;

    public void Update (float _dt) {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Physics.Raycast(ray, out hit);

            if (hit.collider && hit.collider.CompareTag(GV.UNIT_TAG_SOLDIER)) {
                hasClicked = true;
                UnitManager.Instance.SelectUnit(hit.collider.transform);
            } else if (hasClicked && hit.collider && hit.collider.CompareTag(GV.CELL_MOVING_TAG)) {
                UnitManager.Instance.MoveUnit(hit.collider.transform);
            } else {
                hasClicked = false;
                UnitManager.Instance.DeselectUnit();
            }
        }
    }
}
