using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Titulo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("CambiarFase", 5.0f);
    }

    
    void CambiarFase()
    {
        SceneManager.LoadScene("Fase1");
    }
}
