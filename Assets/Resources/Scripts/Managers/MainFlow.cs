using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlow : MonoBehaviour {

    public int gridSize = 10;
    public Transform units;

	// Use this for initialization
	void Start () {
        GridManager.Instance.Init(gridSize, units);
        UnitManager.Instance.Init();
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        InputManager.Instance.Update(dt);
	}
}
