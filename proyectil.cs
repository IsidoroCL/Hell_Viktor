using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class proyectil : MonoBehaviour
{

    private int contadorMortal;
    public float vel;

    void Start()
    {
        //contadorMortal = 0;
        //vel = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //transform.LookAt(mouseOnScreen);
        transform.Translate(new Vector3(vel, 0.0f, 0.0f));
        if (transform.localPosition.z > 20)
        {
            vel = Mathf.Abs(vel); //Si está demasiado lejos se vuelve siempre negativo
        } 
        else if (transform.localPosition.z < -0.5)
        {
            vel = -Mathf.Abs(vel); //Si está por detrás del personaje, el valor se vuelve siempre positivo
            
        } 
        
        //contadorMortal++;
        //if (contadorMortal > 7) { Destroy(gameObject); } //Autodestrucción
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Muro" || col.gameObject.tag == "Puerta")
        {
            transform.localPosition = new Vector3(0.0f, 0.0f, -0.4f);
            /*
            Instantiate(gameObject, new Vector3(0.0f, 0.0f), transform.rotation);
            Destroy(gameObject);*/
        }

    }

}
