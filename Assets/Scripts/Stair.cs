using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.SendMessage("CanUpDown");
        }
        else if (other.gameObject.tag.Equals("Barrel"))
        {
            if (transform.position.y < other.transform.position.y)
            {
                other.gameObject.SendMessage("CanDown");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            other.gameObject.SendMessage("CantUpDown");
        }
    }
}
