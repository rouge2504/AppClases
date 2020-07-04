using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField nombre;
    public InputField apellido;
    public InputField mail;
    public InputField password;

    public string validacion_Mail;
    public string validacion_Password;

    private int validacion;
    // Start is called before the first frame update
    void Start()
    {


        validacion = PlayerPrefs.GetInt("Validacion");
        print(validacion);
        if (validacion == 1)
        {
            print("Acceso Concedido");
            SceneManager.LoadScene("Principal");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarEscena()
    {
        string nombre_str = nombre.text;
        string apellido_str = apellido.text;
        string mail_str = mail.text;
        string password_str = password.text;

        print("Nombre: " + nombre_str + 
            " Apellido: " + apellido_str + 
            " Mail: " + mail_str + 
            " Password: " + password_str);

        if ((mail_str == validacion_Mail) && (password_str == validacion_Password))
        {
            print("Acceso Concedido");
            PlayerPrefs.SetInt("Validacion", 1);
            SceneManager.LoadScene("Principal");
        }
        else
        {
            print("No esta correcto algunos de los campos");
        }


    }
}
