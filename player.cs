using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public float vel;
    public int stamina;
    public int disminuye_stamina;

    public int stamina_max;
    public bool puedeCorrer;
    
    //Para gestionar cámara y sonidos
    private Vector3 lastPosition;
    private bool isMoving;
    private bool corriendo;
    private bool reproducirAndar;
    private bool reproducirCorrer;

    public GameObject linterna;
    public GameObject model;
    public GameObject dead;
    public GameObject bar;
    public int tenerLlave;

    private Animator anim;
    private Rigidbody2D rb;
    private AudioSource[] source;
    
    // Start is called before the first frame update
    void Start()
    {
        //Framerate
        Application.targetFrameRate = 60;

        anim = GetComponentInChildren<Animator>();
        anim.SetInteger("Estado", 0);
        rb = GetComponent<Rigidbody2D>();
        source = GetComponents<AudioSource>();
        
        bar.SetActive(false);

        stamina_max = stamina;
        tenerLlave = 0;
        puedeCorrer = true;
        isMoving = false;
        reproducirAndar = true;
    }

    // Update is called once per frame
    void Update()
    {

        lastPosition = transform.position;
        //Movimiento
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) 
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina>0 && puedeCorrer)
            {
                transform.Translate(new Vector3((-vel)*2, 0.0f));
                stamina = stamina - disminuye_stamina;
                //Debug.Log("Stamina: " + stamina);
                anim.SetInteger("Estado", 2);
                corriendo = true;
                bar.SetActive(true);
            } else
            {
                transform.Translate(new Vector3(-vel, 0.0f));
                anim.SetInteger("Estado", 1);
                corriendo = false;
                if (puedeCorrer) { bar.SetActive(false); }
                
            }
            
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && puedeCorrer)
            {
                transform.Translate(new Vector3((vel) * 2, 0.0f));
                stamina = stamina - disminuye_stamina;
                //Debug.Log("Stamina: " + stamina);
                anim.SetInteger("Estado", 2);
                corriendo = true;
                bar.SetActive(true);
            }
            else
            {
                transform.Translate(new Vector3(vel, 0.0f));
                anim.SetInteger("Estado", 1);
                corriendo = false;
                if (puedeCorrer) { bar.SetActive(false); }
            }
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && puedeCorrer)
            {
                transform.Translate(new Vector3(0.0f, (vel) * 2));
                stamina = stamina - disminuye_stamina;
                //Debug.Log("Stamina: " + stamina);
                anim.SetInteger("Estado", 2);
                corriendo = true;
                bar.SetActive(true);
            }
            else
            {
                transform.Translate(new Vector3(0.0f, vel));
                anim.SetInteger("Estado", 1);
                corriendo = false;
                if (puedeCorrer) { bar.SetActive(false); }
            }
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftShift) && stamina > 0 && puedeCorrer)
            {
                transform.Translate(new Vector3(0.0f, (-vel) * 2));
                stamina = stamina - disminuye_stamina;
                //Debug.Log("Stamina: " + stamina);
                anim.SetInteger("Estado", 2);
                corriendo = true;
                bar.SetActive(true);
            }
            else
            {
                transform.Translate(new Vector3(0.0f, -vel));
                anim.SetInteger("Estado", 1);
                corriendo = false;
                if (puedeCorrer) { bar.SetActive(false); }
            }
        }

        //Si el objeto se para o no está en movimiento
        //Gestiona la animación y el sonido
        if (transform.position == lastPosition)
        {
            anim.SetInteger("Estado", 0);
            source[0].Stop();
            source[1].Stop();
            source[2].Stop();
            isMoving = false;
        } else
        {
            //Andando o corriendo
            isMoving = true;
            if (corriendo)
            {
                if (reproducirCorrer)
                {
                    reproducirCorrer = false;
                    StopAllCoroutines();
                    StartCoroutine(sonidoAndar(0.3f));

                }
                
            }
            else 
            {
                if (reproducirAndar)
                {
                    reproducirAndar = false;
                    StopAllCoroutines();
                    StartCoroutine(sonidoAndar(0.7f));
                }
            }
            
        }

        //Recuperacion stamina
        if (Input.GetKey(KeyCode.LeftShift))
        {

        } 
        else if(stamina < stamina_max)
        {
            stamina++;
            Debug.Log("Stamina: " + stamina);
            if (!puedeCorrer && stamina > (stamina_max/1.5))
            {
                puedeCorrer = true;
            }
        }
        
        if (stamina <= 0)
        {
            puedeCorrer = false;
        }

        if (!puedeCorrer) { bar.SetActive(true); }

        //Encender y apagar la linterna
        if (Input.GetMouseButtonDown(0))
        {
            if (linterna.activeSelf)
            {
                linterna.SetActive(false);
            } else
            {
                linterna.SetActive(true);
            }
        }

        rotacionHijos();

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemigo")
        {
            //Personaje muere, crear gameobject de muerte
            Instantiate(dead, new Vector3(transform.position.x, transform.position.y, -4.0f), Quaternion.identity);
            //Animacion de muerte
            gameObject.SetActive(false);
            
        }

        if (col.gameObject.tag == "Puerta")
        {
            if (tenerLlave > 0)
            {
                //col.gameObject.SetActive(false);
                col.gameObject.SendMessage("Abrir");
                tenerLlave -= 1;
            }
        }

        if (col.gameObject.tag == "Llave")
        {
            //recoger llave y desactivar
            tenerLlave += 1;
            Debug.Log("Consigue la llave");
            source[2].Play();
            col.gameObject.SetActive(false);
        }

        if (col.gameObject.tag == "Salida")
        {
            int id_siguienteEscena = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(id_siguienteEscena);
        }

    }


    //Corutina para hacer los pasos al andar
    IEnumerator sonidoAndar(float v)
    {
        for (int i = 0; i < 2; i++)
        {
            if (isMoving)
            {
                source[i].Play();
            }

            yield return new WaitForSeconds(v);
        }
        reproducirAndar = true;
        reproducirCorrer = true;

    }

    void rotacionHijos()
    {
        //Rotacion del personaje según mouse
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(linterna.transform.position);
        Vector2 positionOnScreen_m = Camera.main.WorldToViewportPoint(model.transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);
        float angle2 = AngleBetweenTwoPoints(positionOnScreen_m, mouseOnScreen);

        //Ta Daaa
        linterna.transform.rotation = Quaternion.Euler(new Vector3(angle, -90.0f, 0f));
        model.transform.rotation = Quaternion.Euler(new Vector3(-angle2 - 180.0f, 90.0f, -90.0f));
    }

}
