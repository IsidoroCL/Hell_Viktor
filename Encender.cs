using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encender : MonoBehaviour
{
    public Light luz;
    // Start is called before the first frame update
    void Start()
    {
        luz = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            luz.enabled = true;
        }
    }
}
