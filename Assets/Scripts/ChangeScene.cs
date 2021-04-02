using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Para usar SceneManager
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Cambiar de escena, es público para que otros objetos lo puedan usar
    public void changeToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
