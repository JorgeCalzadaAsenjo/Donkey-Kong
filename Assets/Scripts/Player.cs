using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private float salto;
    private int velocidad = 1;
    private int velocidadSalto = 25;
    private float alturaPersonaje;
    Vector2 startPosition;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if ((transform.position.x > -2 && horizontal < 0) || (transform.position.x < 2 && horizontal > 0))
        {
            transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

            if (horizontal > 0.1f)
            {
                anim.Play("RunPlayerRight");
            }
            else if (horizontal < -0.1f)
            {
                anim.Play("RunPlayerLeft");
            }
        }

        salto = Input.GetAxis("Jump");

        if (salto > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -0.25f));

            float distanciaAlSuelo = hit.distance;
            bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
            if (tocandoElSuelo)
            {
                Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);
                GetComponent<Rigidbody2D>().AddForce(fuerzaSalto);
            }
        }

        vertical = Input.GetAxis("Vertical");

        
    }
}
