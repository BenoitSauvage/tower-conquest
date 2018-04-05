using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {

    float hp = 100;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow)) {
            ManageKillUnit( hp--, 100);
        }

    }

    void SetActiveUnit(int _GetChild, bool _SetActive)
    {
        foreach (Transform child in transform) {
            transform.GetChild(_GetChild).gameObject.SetActive(_SetActive);
        }
    }

    void DestroyUnit()
    {
        foreach (Transform child in transform)
        {
            Destroy(gameObject);
        }
    }

    void ManageKillUnit(float CurrentHp, float maxHp) {
        float convertCurrentHp = 100 * (CurrentHp / maxHp);

        if (convertCurrentHp <= 100 && convertCurrentHp > 85)
        {
            //_____°-°_____
        }
        else if (convertCurrentHp <= 85 && convertCurrentHp > 65)
        {
            SetActiveUnit(0, false);
        }
        else if (convertCurrentHp <= 65 && convertCurrentHp > 50)
        {
            SetActiveUnit(0, true);
            SetActiveUnit(3, false);
            SetActiveUnit(1, false);
        }
        else if (convertCurrentHp <= 50 && convertCurrentHp > 35)
        {
            SetActiveUnit(0, false);
        }
        else if (convertCurrentHp <= 35 && convertCurrentHp > 15)
        {
            SetActiveUnit(0, true);
            SetActiveUnit(2, false);
            SetActiveUnit(4, false);
        }
        else
        {
            DestroyUnit();
        }
    }
    //__________________________________________________________________To Reorganize for revive
    void ManageReviveUnit(float CurrentHp, float maxHp)
    {
        float convertCurrentHp = 100 * (CurrentHp / maxHp);

        if (convertCurrentHp <= 100 && convertCurrentHp > 85)
        {
            //_____°-°_____
        }
        else if (convertCurrentHp <= 85 && convertCurrentHp > 65)
        {
            SetActiveUnit(0, false);
        }
        else if (convertCurrentHp <= 65 && convertCurrentHp > 50)
        {
            SetActiveUnit(0, true);
            SetActiveUnit(3, false);
            SetActiveUnit(1, false);
        }
        else if (convertCurrentHp <= 50 && convertCurrentHp > 35)
        {
            SetActiveUnit(0, false);
        }
        else if (convertCurrentHp <= 35 && convertCurrentHp > 15)
        {
            SetActiveUnit(0, true);
            SetActiveUnit(2, false);
            SetActiveUnit(4, false);
        }
        else
        {
            DestroyUnit();
        }
    }
}
