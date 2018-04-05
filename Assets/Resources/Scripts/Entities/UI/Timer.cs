using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    RectTransform rect;

	void Start() {
        rect = GetComponent<RectTransform>();
        rect.anchorMax = new Vector2(0, 1);
	}

	// Update is called once per frame
	void Update () {
        float x = GameManager.Instance.GetCurrentTurnTime() / GV.MAX_TURN_DURATION;
        rect.anchorMax = new Vector2(x, 1);
	}
}
