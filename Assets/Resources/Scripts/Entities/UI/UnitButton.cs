using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour {

    public GV.UNIT_TYPE unitType;

    protected float cost;
    protected bool isEnabled = true;

    Image image;

	private void Start() {
        image = GetComponent<Image>();
	}

	public void OnUnitButtonClick() {
        if (isEnabled) {
            InputManager.Instance.isPlacingUnit = true;
            GridManager.Instance.RemovePlacingCell();
            GridManager.Instance.CreateGhostUnit(unitType);
            GridManager.Instance.ShowPlayerCells();
        }
    }

    public void Toggle (bool _value) {
        isEnabled = _value;
        /*
        if (!isEnabled)
            image.sprite = Resources.Load<Sprite>("Images/unit_button_disable");
        else
            image.sprite = Resources.Load<Sprite>("Images/unit_button_enable");
        */
    }

    public float GetCost() {
        return cost;
    }
}
