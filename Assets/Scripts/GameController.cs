using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private int points;
    private int lives;
    private int nivelActual = 1;
    private int nivelMasAlto = 2;
    [SerializeField] Text data;
    

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        lives = 5;
        printData();
    }

    public void LostLive()
    {
        lives--;
        printData();
        if (lives == 0)
        {
            StartCoroutine("quitGame");
        }
    }

    IEnumerator quitGame()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
    }

    public void SumPoints()
    {
        points += 80;
        printData();
    }

    private void printData()
    {
        data.text = "Vidas: " + lives + "    Puntos: " + points;
    }
}
