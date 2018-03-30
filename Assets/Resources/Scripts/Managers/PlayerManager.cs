using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager {
    
    #region singleton
    private static PlayerManager instance;

    private PlayerManager() { }

    public static PlayerManager Instance {
        get {
            if (instance == null)
                instance = new PlayerManager();

            return instance;
        }
    }
    #endregion singleton

    private Dictionary<float, Player> players = new Dictionary<float, Player>();

    public void Init() {
        Player player_1 = new Player();
        Player player_2 = new Player();

        players.Add(1f, player_1);
        players.Add(2f, player_2);
    }

    public void NextTurn (float _turn) {
        if (_turn > 1)
            players[GameManager.Instance.GetCurrentPlayer()].UpdateCoins(GV.NEW_TURN_COINS);
    }

    public float GetPlayerCoins (float _playerNumber) {
        return players[_playerNumber].GetCoins();
    }

    public void UpdateCoins (float _value) {
        players[GameManager.Instance.GetCurrentPlayer()].UpdateCoins(-_value);
    }
}
