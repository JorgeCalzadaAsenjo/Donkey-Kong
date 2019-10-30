using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public void Cross()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void DontCross()
    {
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
