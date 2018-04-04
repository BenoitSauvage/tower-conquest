using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager {

    #region singleton
    private static CameraManager instance;

    private CameraManager() { }

    public static CameraManager Instance {
        get {
            if (instance == null)
                instance = new CameraManager();

            return instance;
        }
    }
    #endregion singleton

    private Transform camPivot;
    private float speed, angle = 1f;
    private Vector3 cameraPos;

    private bool turn = true;
    private bool hasToRotate = false;

    public void Init (Transform _camPivot, float _speed) {
        camPivot = _camPivot;
        speed = _speed;
    }

    public void Update(float _dt) {
        if (hasToRotate) {
            if (turn) {
                angle = camPivot.rotation.eulerAngles.y;

                if (angle >= 0 && angle < 180)
                    camPivot.Rotate(0, _dt * speed, 0);

                if (angle > 175) {
                    camPivot.eulerAngles = new Vector3(0, 180, 0);
                    turn = false;
                    hasToRotate = false;
                    cameraPos = Camera.main.transform.position;
                    GameManager.Instance.NextTurn();
                }
            } else {
                angle = camPivot.rotation.eulerAngles.y;

                if (angle >= 180 && angle < 360)
                    camPivot.Rotate(0, _dt * speed, 0);

                if (angle >= 355) {
                    camPivot.eulerAngles = new Vector3(0, 0, 0);
                    turn = true;
                    hasToRotate = false;
                    cameraPos = Camera.main.transform.position;
                    GameManager.Instance.NextTurn();
                }
            }
        }
    }

    public void RotateCamera () {
        InputManager.Instance.NextTurn();
        GridManager.Instance.NextTurn();
        hasToRotate = true;
    }

    public void DampCamera (float _dt, Transform _target) {
        var targetRotation = Quaternion.LookRotation(_target.transform.position - cameraPos);
        Camera.main.transform.rotation = Quaternion.Slerp(
            Camera.main.transform.rotation, targetRotation, (_dt / 2) 
        );

        Camera.main.transform.position = Vector3.Lerp(
            cameraPos, (_target.position / 2), (_dt / 2) / GV.ENDGAME_ANIMATION_DURATION
        );
    }
}
