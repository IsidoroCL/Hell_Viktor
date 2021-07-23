using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public float vel;
    public GameObject player;
    public int tiempoEnfadado;
    public bool ortogonal;

    private int direccion;
    public bool enfadado;
    private int contadorEnfado;
    private bool reproducirAndar;
    private bool reproducirCorrer;

    private Animator anim;
    private AudioSource[] sources;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        sources = GetComponents<AudioSource>();
        
        direccion = Random.Range(0, 4);
        enfadado = false;
        reproducirAndar = true;
        reproducirCorrer = true;
        //Fijamos direccion inicial
        cambiarDireccion();
    }

    // Update is called once per frame
    void Update()
    {
        if (enfadado)
        {
            //Si está enfadado, se mueve más rápido y empieza a contar hacia abajo el contador
            transform.Translate(new Vector3((vel*4), 0.0f));
            anim.SetInteger("moving", 20);//walk/run/moving
            contadorEnfado--;
            if (contadorEnfado <= 0)
            {
                enfadado = false;
                direccion = Random.Range(0, 4);
                cambiarDireccion();
            }
            //Se para sonido al andar y empieza sonido al correr
            if (reproducirCorrer)
            {
                StopAllCoroutines();
                StartCoroutine(sonidoAndar(0.3f));
                reproducirCorrer = false;
            }
        } else
        {
            transform.Translate(new Vector3(vel, 0.0f));
            anim.SetInteger("moving", 1);//walk/run/moving
            //Empezamos el sonido al andar
            if (reproducirAndar)
            {
                StopAllCoroutines();
                StartCoroutine(sonidoAndar(0.7f));
                reproducirAndar = false;
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Muro" || col.gameObject.tag == "Puerta" || col.gameObject.tag == "Enemigo")
        {
            if (enfadado)
            {
                //direccionOrtogonal();
                perseguirPlayer();
            } 
            else
            {
                switch (direccion)
                {
                    case 0:
                        transform.Translate(new Vector3(-0.5f, 0.0f));
                        direccion = Random.Range(2, 4);
                        break;
                    case 1:
                        transform.Translate(new Vector3(-0.5f, 0.0f));
                        direccion = Random.Range(2, 4);
                        break;
                    case 2:
                        transform.Translate(new Vector3(-0.5f, 0.0f));
                        direccion = Random.Range(0, 2);
                        break;
                    case 3:
                        transform.Translate(new Vector3(-0.5f, 0.0f));
                        direccion = Random.Range(0, 2);
                        break;
                }
                cambiarDireccion();
            }
            
        }

        
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Linterna")
        {
            //Si se ilumina con la linterna se enfada y persigue al jugador
            enfadado = true;
            contadorEnfado = tiempoEnfadado;
            Debug.Log("Monstruo iluminado");
            perseguirPlayer();
            if (ortogonal)
            {
                direccionOrtogonal();
            }   
        }
    }

    private void cambiarDireccion()
    {
        //Cuando choca contra una pared cambia de dirección
        switch (direccion)
        {
            case 0: //Derecha
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
                break;
            case 1: //Izquierda
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
                break;
            case 2: //Abajo
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
                break;
            case 3: //Arriba
                transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
                break;
        }
    }

    public void perseguirPlayer()
    {

        //posicion de jugador y enemigo
        //Vector2 positionPlayer = Camera.main.WorldToViewportPoint(player.transform.position);
        //Vector2 positionEnemigo = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 positionPlayer = player.transform.position;
        Vector2 positionEnemigo = transform.position;
        float angle = AngleBetweenTwoPoints(positionEnemigo, positionPlayer);

        //Giro al enemigo según player
        //Hay que añadir un -180 al angulo para que se gire correctamente el enemigo
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, angle - 180.0f));
        Debug.Log("Player: " + positionPlayer + "Enemigo: " + positionEnemigo + "Angulo: " + angle);
    }

    public void direccionOrtogonal()
    {
        //Rota a la dirección más cercana entre arriba, derecha, abajo e izquierda
        float zAngle = transform.rotation.eulerAngles.z;
        
        if (zAngle < 46 || zAngle > 315)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f));
        } 
        else if (zAngle < 136 && zAngle > 45)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f));
        }
        else if (zAngle < 226 && zAngle > 135)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f));
        }
        else if (zAngle < 316 && zAngle > 225) 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 270.0f));
        }
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    IEnumerator sonidoAndar(float v)
    {
        for (int i = 0; i < sources.Length; i++)
        {
            sources[i].Play();
            yield return new WaitForSeconds(v);
        }
        reproducirAndar = true;
        reproducirCorrer = true;
    }
}
