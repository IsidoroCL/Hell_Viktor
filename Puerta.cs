using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{

    public bool abrirse = false;
    private Collider2D c2d;
    private AudioSource source;

    private void Start()
    {
        c2d = GetComponent<Collider2D>();
        source = GetComponent<AudioSource>();
    }


    void Update()
    {
        if (abrirse)
        {
            if (transform.position.z < 2)
            {
                transform.Translate(new Vector3(0.0f, 0.0f, 0.03f));
            } 
            else
            {
                c2d.enabled = false;

                abrirse = false;
            }
            
        }
    }

    public void Abrir()
    {
        abrirse = true;
        source.Play();
    }
}
