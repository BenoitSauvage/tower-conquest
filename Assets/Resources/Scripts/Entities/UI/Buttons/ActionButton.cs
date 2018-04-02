using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour {

    public GV.ACTION_TYPE actionType;

    private Image image;

    public void Start() {
        image = GetComponent<Image>();
        UpdateButton();
	}

	public void OnActionButtonClick() {
        InputManager.Instance.UpdateActionType(actionType);
    }

    public void UpdateButton() {
        GV.ACTION_TYPE playerActionType = InputManager.Instance.GetActionType();

        Color color = image.color;
        color.a = (playerActionType == actionType) ? .2f : 1f;
        image.color = color;
    }
}
