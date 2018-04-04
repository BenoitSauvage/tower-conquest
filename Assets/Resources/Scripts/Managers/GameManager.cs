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

    private float winner = 0f;

    private Transform player1Castle, player2Castle;

    public void Init (Transform _player1, Transform _player2) {
        player1Castle = _player1;
        player2Castle = _player2;
    }

    public void Update (float _dt) {
        turnDuration += _dt;

        if (turnDuration >= GV.MAX_TURN_DURATION) {
            // turnDuration = 0f;
            CameraManager.Instance.RotateCamera();
        }
    }

    public float GetWinner () {
        return winner;
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