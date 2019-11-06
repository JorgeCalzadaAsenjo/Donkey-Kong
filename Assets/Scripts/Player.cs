using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool canUpDown;
    private bool isUpDown;
    private float posicionY;
    private bool withHammer;
    private Coroutine deleteHammer;
    Floor[] floors;
    [SerializeField] bool enAire;

    void Start()
    {
        startPosition = transform.position;
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y * 2;
        anim = gameObject.GetComponent<Animator>();
        canUpDown = false;
        isUpDown = false;
        floors = FindObjectsOfType<Floor>();
        changeFloor();
        posicionY = transform.position.y;
        withHammer = false;
    }

    void Update()
    {
        if (!isUpDown)
        {
            horizontal = Input.GetAxis("Horizontal");

            if ((transform.position.x > -2 && horizontal < 0) || (transform.position.x < 2 && horizontal > 0))
            {
                transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

                if (horizontal > 0.1f)
                {
                    if (withHammer)
                    {
                        anim.Play("RunPlayerHammerRight");
                    }
                    else
                    {
                        anim.Play("RunPlayerRight");
                    }
                }
                else if (horizontal < -0.1f)
                {
                    if (withHammer)
                    {
                        anim.Play("RunPlayerHammerLeft");
                    }
                    else
                    {
                        anim.Play("RunPlayerLeft");
                    }
                }
            }

            salto = Input.GetAxis("Jump");

            if (salto > 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));

                float distanciaAlSuelo = hit.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
                if (tocandoElSuelo)
                {
                    Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);
                    GetComponent<Rigidbody2D>().AddForce(fuerzaSalto);
                }
            }
        }

        if (canUpDown)
        {
            vertical = Input.GetAxis("Vertical");

            if (vertical != 0)
            {
                deleteHammer = null;
                withHammer = false;
                isUpDown = true;
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                transform.Translate(0, vertical * velocidad * Time.deltaTime, 0);
                anim.Play("RunPlayerUpDown");
                posicionY -= 0.5f;
                changeFloor();

                int floor = 0;
                while (isUpDown && floor < floors.Length)
                {
                    if ((transform.position.y - (GetComponent<Collider2D>().bounds.size.y)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) > -0.03 &&
                        (transform.position.y - (GetComponent<Collider2D>().bounds.size.y)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) < 0.03)
                    {
                        isUpDown = false;
                        anim.Play("RunPlayerRight");
                    }

                    floor++;
                }
            }

            
        }

        

        if (GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            posicionY = transform.position.y;
            changeFloor();
        }

        //Anular controles cuando este arriba aunque no este en una escalera
        /*if (isUpDown && !canUpDown)
        {
            int floor = 0;
            while (isUpDown && floor < floors.Length)
            {
                if ((transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) > -0.02 &&
                    (transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) < 0.02)
                {
                    isUpDown = false;
                    anim.Play("RunPlayerRight");
                }

                floor++;
            }
        }*/

    }

    public void FinishGame()
    {
        StartCoroutine("Pause");
        SceneManager.LoadScene("Menu");
    }

    public void CanUpDown()
    {
        canUpDown = true;

        /*int floor = 0;
        enAire = true;
        while (enAire && floor < floors.Length)
        {
            if ((transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) > -0.02 &&
                (transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (floors[floor].transform.position.y + (floors[floor].GetComponent<Collider2D>().bounds.size.y / 2)) < 0.02)
            {
                enAire = false;
            }

            floor++;
        }

        if (enAire)
        {
            isUpDown = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }*/

        /*bool tocaSuelo = false;

        foreach (Floor f in floors)
        {
            Debug.Log((transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (f.transform.position.y + (f.GetComponent<Collider2D>().bounds.size.y / 2)));
            if ((transform.position.y - (GetComponent<Collider2D>().bounds.size.y / 2)) - (f.transform.position.y + (f.GetComponent<Collider2D>().bounds.size.y / 2)) == 0)
            {
                isUpDown = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
                tocaSuelo = true;
            }
        }

        if (!tocaSuelo)
        {
            isUpDown = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }*/
    }

    public void CantUpDown()
    {
        posicionY = transform.position.y;
        isUpDown = false;
        canUpDown = false;
        GetComponent<Rigidbody2D>().gravityScale = 1;
        changeFloor();
    }

    public void catchHammer()
    {
        withHammer = true;
        deleteHammer = StartCoroutine(quitHammer());
    }

    IEnumerator quitHammer()
    {
        yield return new WaitForSeconds(10);
        withHammer = false;
        anim.Play("StaticPlayerRight");
    }

    public void TouchedBarrel(BarrelImage barrel)
    {
        StartCoroutine(Pause());
        if (withHammer)
        {
            Destroy(barrel.transform.parent.gameObject);
            FindObjectOfType<GameController>().SendMessage("SumPoints");
        }
        else
        {
            FindObjectOfType<GameController>().SendMessage("LostLive");
        }
    }

    IEnumerator Pause()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1;
    }

    private void changeFloor()
    {
        //f.transform.position.y + f.GetComponent<Collider2D>().bounds.size.y < transform.position.y - GetComponent<Collider2D>().bounds.size.y
        //f.transform.position.y  < transform.position.y
        
        foreach (Floor f in floors)
        {
            if (f.transform.position.y < posicionY)
            {
                f.SendMessage("DontCross");
            }
            else
            {
                f.SendMessage("Cross");
            }
        }
    }
}
