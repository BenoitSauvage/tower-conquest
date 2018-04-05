using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour {

    [HideInInspector]
    public GV.UNIT_TYPE unitType;

    protected float cost;
    protected bool isEnabled = true;

    Image image;

	public virtual void Init() {
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

        Color color = image.color;
        color.a = (isEnabled) ? 1f : .2f;
        image.color = color;
    }

    public float GetCost() {
        return cost;
    }
}
