﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            other.gameObject.SendMessage("catchHammer");
        }
    }
}
