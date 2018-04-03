using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {

    public void StartScene (string _scene) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);
    }
}
