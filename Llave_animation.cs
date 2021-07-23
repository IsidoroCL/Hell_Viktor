using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave_animation : MonoBehaviour
{
    public float vel_rotacion = 1.0f;
    public float vel_move = 0.1f;
    public int rango_move = 105;

    private float move = 0.0f;
    private AudioSource source;

    private void Start()
    {
        //Inicio todas las variables en los valores óptimos probados
        vel_rotacion = 1.0f;
        vel_move = 0.1f;
        rango_move = 105;

        move = 0.0f;

        source = GetComponent<AudioSource>();
}
    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 0.0f, vel_rotacion));
        transform.Translate(new Vector3(0.0f, 0.0f, (Mathf.Sin(move)/rango_move)));
        move += vel_move;
    }

    /*private void OnDisable()
    {
        source.Play();
    }*/

    
}
