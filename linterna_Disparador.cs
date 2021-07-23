using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linterna_Disparador : MonoBehaviour
{
    //Dispara colisionLinterna para activar a los enemigos
    public GameObject disparador;
    private float zRotation;

    // Update is called once per frame
    void Update()
    {
        
        Instantiate(disparador, transform.position, Quaternion.identity);
        
    }
}
