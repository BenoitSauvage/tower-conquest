using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager {

    #region singleton
    private static GameManager instance;

    private GameManager() { }

    public static GameManager Instance {
        get {
            if (instance == null)
                instance = new GameManager();

            return instance;
        }
    }
    #endregion singleton

    private float turnCount = 1;
    private float turnDuration = 0f;

    private float playerPlaying = 1;

    public void Update (float _dt) {
        turnDuration += _dt;

        if (turnDuration >= GV.MAX_TURN_DURATION) {
            turnDuration = 0f;
            NextTurn();
        }
    }

    public float GetTurnCount () {
        return turnCount;
    }

    public float GetCurrentPlayer () {
        return playerPlaying;
    }

    public float GetCurrentTurnTime () {
        return turnDuration;
    }

    public void NextTurn () {
        if (playerPlaying >= GV.MAX_PLAYER) {
            playerPlaying = 1;
            turnCount += 1;
        } else
            playerPlaying += 1;

        InputManager.Instance.NextTurn();
        UnitManager.Instance.NextTurn();
        GridManager.Instance.NextTurn();
    }
}