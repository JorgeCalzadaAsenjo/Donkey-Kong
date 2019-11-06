using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform prefabBarrel;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(throwBarrel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator throwBarrel()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(throwBarrel1());
    }

    IEnumerator throwBarrel1()
    {
        Instantiate(prefabBarrel, new Vector3(-0.8f, 1.2f, 0), Quaternion.identity);
        yield return new WaitForSeconds(2);
        StartCoroutine(throwBarrel1());
    }
}
