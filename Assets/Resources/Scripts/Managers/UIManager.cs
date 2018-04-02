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

    private Text turn, player, coins;
    private RectTransform buttons;

    public void Init(Text _turn, Text _player, Text _coins, RectTransform _buttons) {
        turn = _turn;
        player = _player;
        coins = _coins;

        buttons = _buttons;

        UpdatePlayerText(GameManager.Instance.GetCurrentPlayer());
        UpdateCoinsText(PlayerManager.Instance.GetPlayerCoins(GameManager.Instance.GetCurrentPlayer()));
        InitButtons();
        UpdateButtons(GameManager.Instance.GetCurrentPlayer());
    }

    public void NextTurn (float _turn, float _player) {
        UpdateTurnText(_turn);
        UpdatePlayerText(_player);

        UpdateCoinsText(PlayerManager.Instance.GetPlayerCoins(_player));
        UpdateButtons(_player);
    }

    public void DisableButton(GV.UNIT_TYPE _type) {
        foreach (Transform button in buttons) {
            UnitButton btn = button.GetComponent<UnitButton>();
            if (btn.unitType == _type)
                btn.Toggle(false);
        }
    }

    private void UpdateTurnText (float _turn) {
        turn.text = "TURN : " + _turn;
    }

    private void UpdatePlayerText (float _player) {
        player.text = "PLAYER " + _player;
    }

    private void UpdateCoinsText (float _coins) {
        coins.text = "COINS : " + (int)_coins;
    }

    public void UpdatePlayerInfos () {
        UpdateCoinsText(PlayerManager.Instance.GetPlayerCoins(GameManager.Instance.GetCurrentPlayer()));
        UpdateButtons(GameManager.Instance.GetCurrentPlayer());
    }

    private void InitButtons () {
        foreach (Transform button in buttons) {
            UnitButton btn = button.GetComponent<UnitButton>();
            btn.Init();
        }
    }

    private void UpdateButtons (float _player) {
        float playerCoins = PlayerManager.Instance.GetPlayerCoins(_player);

        foreach (Transform button in buttons) {
            UnitButton btn = button.GetComponent<UnitButton>();
            if (btn.GetCost() > playerCoins)
                btn.Toggle(false);
            else
                btn.Toggle(true);
        }
    }


}