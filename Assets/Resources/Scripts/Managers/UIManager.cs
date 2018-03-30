using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager {

    #region singleton
    private static UIManager instance;

    private UIManager() { }

    public static UIManager Instance {
        get {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }
    }
    #endregion singleton

    private Text player, coins;

    public void Init (Text _player, Text _coins) {
        player = _player;
        coins = _coins;
    }

    public void UpdatePlayerText (float _player) {
        player.text = "PLAYER " + _player;
    }

    public void UpdateCoinsText (float _coins) {
        coins.text = "COINS : " + _coins;
    }
}