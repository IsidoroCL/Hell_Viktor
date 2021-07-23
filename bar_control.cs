using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bar_control : MonoBehaviour
{
    private player p;
    private SpriteRenderer sr;
    
    // Start is called before the first frame update
    void Start()
    {
        p = GetComponentInParent<player>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Cambia el tamaño de la barra según el nivel de stamina
        float relacion_stamina = (float)p.stamina / p.stamina_max;
        transform.localScale = new Vector3(relacion_stamina, 0.1f, 1.0f);
        if (p.puedeCorrer)
        {
            sr.color = Color.green;
        } else
        {
            sr.color = Color.red;
        }
    }
}
