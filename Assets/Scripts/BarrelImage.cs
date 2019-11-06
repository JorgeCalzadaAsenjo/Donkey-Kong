using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelImage : MonoBehaviour
{
    private bool producesDamage;
    // Start is called before the first frame update
    void Start()
    {
        producesDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player") && producesDamage)
        {
            other.SendMessage("TouchedBarrel", this);
            producesDamage = false;
        }
    }
}
