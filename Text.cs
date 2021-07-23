using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Text : MonoBehaviour
{
    public bool seDesvanece;
    private SpriteRenderer sprRend;
    // Start is called before the first frame update
    void Start()
    {
        sprRend = GetComponent<SpriteRenderer>();
        StartCoroutine(fadeOut(sprRend, 2.0f, seDesvanece));
    }

    
    // Update is called once per frame
    void Update()
    {
        //Pulsar R para resetear la pantalla
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator fadeOut(SpriteRenderer MyRenderer, float duration, bool outOrIn)
    {
        float counter = 0;
        float alpha = 0.0f;
        //Get current color
        Color spriteColor = MyRenderer.material.color;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            if (outOrIn) //true se desvanece
            {
                //Fade from 1 to 0
                alpha = Mathf.Lerp(1, 0, counter / duration);
            } 
            else //false aparece
            {
                //Fade from 0 to 1
                alpha = Mathf.Lerp(0, 1, counter / duration);
            }
            

            //Change alpha only
            MyRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
            //Wait for a frame
            yield return null;
        }
    }
}
