using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainFlow : MonoBehaviour {

    [Tooltip("Grid size X * X")]
    public int gridSize = 10;

    [Space(10)]
    [Tooltip("All units on map")]
    public Transform units;

    [Space(10)]
    [Header("UI")]
    [Tooltip("Text showing which player is playing")]
    public Text player;
    [Tooltip("Text showing how mush coins the player has")]
    public Text coins;

	// Use this for initialization
	void Start () {
        UIManager.Instance.Init(player, coins);
        GridManager.Instance.Init(gridSize, units);
        UnitManager.Instance.Init(units);
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        GameManager.Instance.Update(dt);
        InputManager.Instance.Update(dt);
	}
}
