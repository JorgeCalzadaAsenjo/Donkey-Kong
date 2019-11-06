using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStartScene : MonoBehaviour
{
    public void LanzarJuego()
    {
        SceneManager.LoadScene("Level1");
    }
}
