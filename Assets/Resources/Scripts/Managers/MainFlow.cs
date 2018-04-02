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
    [Tooltip("Group holding unit buttons")]
    public RectTransform unitButtons;
    [Tooltip("Text showing which player is playing")]
    public Text player;
    [Tooltip("Text showing how mush coins the player has")]
    public Text coins;
    [Tooltip("Text showing turn number")]
    public Text turn;

    public Button moveButton, attackButton;

	// Use this for initialization
	void Start () {
        PlayerManager.Instance.Init();
        GameManager.Instance.Init();
        GridManager.Instance.Init(gridSize, units);
        UnitManager.Instance.Init(units);

        UIManager.Instance.Init(
            turn, player, coins, unitButtons, moveButton.GetComponent<ActionButton>(), attackButton.GetComponent<ActionButton>()
        );
	}
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;

        InputManager.Instance.Update(dt);
        GameManager.Instance.Update(dt);
	}
}
