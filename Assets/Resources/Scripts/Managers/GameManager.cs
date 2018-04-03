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


    public void Init () {
        // ...
    }

    public void Update (float _dt) {
        turnDuration += _dt;

        if (turnDuration >= GV.MAX_TURN_DURATION) {
            // turnDuration = 0f;
            CameraManager.Instance.RotateCamera();
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
        turnDuration = 0f;

        if (playerPlaying >= GV.MAX_PLAYER) {
            playerPlaying = 1;
            turnCount += 1;
        } else
            playerPlaying += 1;

        InputManager.Instance.NextTurn();
        PlayerManager.Instance.NextTurn(turnCount);
        UnitManager.Instance.NextTurn();
        GridManager.Instance.NextTurn();

        UIManager.Instance.NextTurn(turnCount, playerPlaying);
    }
}