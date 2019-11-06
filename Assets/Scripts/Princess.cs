using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Princess : MonoBehaviour
{
    [SerializeField] Text helpText;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(showHelp());
    }

    IEnumerator showHelp()
    {
        yield return new WaitForSeconds(Random.Range(5.0f, 11.0f));
        helpText.enabled = true;
        StartCoroutine(hideHelp());
    }

    IEnumerator hideHelp()
    {
        yield return new WaitForSeconds(3);
        helpText.enabled = false;
        StartCoroutine(showHelp());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.SendMessage("FinishGame");
        }
    }
}
