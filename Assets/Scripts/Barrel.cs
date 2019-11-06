using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private int desplazamiento = 1;
    private bool cambio = false;
    private Animator anim;
    [SerializeField] bool down;

    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody2D>().velocity.y >= -1)
        {
            if (cambio)
            {
                desplazamiento *= -1;
                cambio = false;

                if (desplazamiento > 0)
                {
                    anim.Play("BarrelA");
                }
                else
                {
                    anim.Play("BarrelB");
                }
            }
            else
            {
                transform.Translate(desplazamiento * Time.deltaTime, 0, 0);
            }

            
        } else
        {
            cambio = true;
        }

        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }

        if (down)
        {
            down = false;
            GetComponent<CircleCollider2D>().isTrigger = true;
            StartCoroutine(returnToNormality());
        }
    }

    public void CanDown()
    {
        down = Random.Range(0, 2) == 1;
    }

    IEnumerator returnToNormality()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<CircleCollider2D>().isTrigger = false;
    }
}
