using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [Header("Imagenes")]
    public Image imagen;
    public Sprite segundoSprite;

    public InputField inputField;

    [Header("Textos")]
    public Text textoMuestra;

    [Header("Pantallas")]
    public GameObject pantalla0;
    public GameObject pantalla1;

    public float contadorParaCambiarPantalla;
    public float limiteParaCambiarPantalla;


    public Image barraDeTiempo;

    // Start is called before the first frame update
    void Start()
    {
        imagen.sprite = segundoSprite;

        textoMuestra.text = "Si!!!!!";
        textoMuestra.fontSize = 50;
    }

    // Update is called once per frame
    void Update()
    {
        /*contadorParaCambiarPantalla += Time.deltaTime;
        if (contadorParaCambiarPantalla > limiteParaCambiarPantalla)
        {
            //contadorParaCambiarPantalla = 0;
            //CambioDePantalla(pantalla0, pantalla1);
            barraDeTiempo.color = new Color(255,0,0);
        }*/

        if (Input.GetKeyUp(KeyCode.A))
        {
            barraDeTiempo.fillAmount += 0.1f;
        }
    }

    public void CambioDePantalla (GameObject pantalla0, GameObject pantalla1)
    {
        pantalla0.SetActive(false);
        pantalla1.SetActive(true);
    }

    public void CerrarSesion()
    {
        PlayerPrefs.DeleteKey("Validacion");
        SceneManager.LoadScene("Login");
    }

}
