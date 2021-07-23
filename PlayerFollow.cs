using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTransform;
    public float velZoomCamera;
    
    private Vector3 current;
    private Vector3 previous;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
        current = followTransform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        previous = current;
        current = followTransform.position;
        Vector3 velocity = (current - previous) / Time.deltaTime;
        //Debug.Log("Velocidad: "+ velocity.magnitude);
        //this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
        if (velocity.magnitude > 1.0f && this.transform.position.z > -12) //Correr - camara zoom out
        {
            this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z - velZoomCamera);
        } 
        else if (velocity.magnitude < 0.1f && this.transform.position.z < -10) //Andar o parado - camar zoom in
        {
            this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z + velZoomCamera);
        }
        else
        {
            this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);
        }
    }
}
