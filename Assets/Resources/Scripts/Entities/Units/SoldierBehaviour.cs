using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour {

    public void UpdateLife (float _life, float _maxLife) {
        ManageKillUnit(_life, _maxLife);
    }

    private void SetActiveUnit(int _GetChild, bool _SetActive) {
        foreach (Transform child in transform)
            transform.GetChild(_GetChild).gameObject.SetActive(_SetActive);
    }

    private void ManageKillUnit(float CurrentHp, float maxHp) {
        float convertCurrentHp = 100 * (CurrentHp / maxHp);

        if (convertCurrentHp <= 100 && convertCurrentHp > 85) {
            SetActiveUnit(0, true);
            SetActiveUnit(1, true);
            SetActiveUnit(2, true);
            SetActiveUnit(3, true);
            SetActiveUnit(4, true);
        } else if (convertCurrentHp <= 85 && convertCurrentHp > 65) {
            SetActiveUnit(0, false);

            SetActiveUnit(1, true);
            SetActiveUnit(2, true);
            SetActiveUnit(3, true);
            SetActiveUnit(4, true);
        } else if (convertCurrentHp <= 65 && convertCurrentHp > 50) {
            SetActiveUnit(2, false);
            SetActiveUnit(4, false);

            SetActiveUnit(0, true);
            SetActiveUnit(1, true);
            SetActiveUnit(3, true);
        } else if (convertCurrentHp <= 50 && convertCurrentHp > 35) {
            SetActiveUnit(0, false);
            SetActiveUnit(1, false);
            SetActiveUnit(3, false);

            SetActiveUnit(2, true);
            SetActiveUnit(4, true);
        } else if (convertCurrentHp <= 35 && convertCurrentHp > 15) {
            SetActiveUnit(0, true);

            SetActiveUnit(1, false);
            SetActiveUnit(2, false);
            SetActiveUnit(3, false);
            SetActiveUnit(4, false);
        }
    }
}
