using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animacion_monstruo : MonoBehaviour   
{

    private Animator anim;
    public GameObject padre;
    public float speed = 6.0f;
    public float runSpeed = 3.0f;
    private float w_sp = 0.0f;
    private float r_sp = 0.0f;

    //public enemigo padre;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        w_sp = speed; //read walk speed
        r_sp = runSpeed; //read run speed

    }

    // Update is called once per frame
    void Update()
    {        
        if (padre.transform.hasChanged)
        {
            anim.SetInteger("moving", 1);//walk/run/moving
        }
        else
        {
            anim.SetInteger("moving", 0);
        }
    }
}
