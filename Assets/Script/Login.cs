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


    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;
    // Start is called before the first frame update
    void Start()
    {

        InitializeFirebase();

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

    public void UsuarioNuevo()
    {
        auth.CreateUserWithEmailAndPasswordAsync(mail.text, password.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    void InitializeFirebase()
    {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && user != null)
            {
                Debug.Log("Signed out " + user.UserId);
            }
            user = auth.CurrentUser;
            if (signedIn)
            {
                /*DebugLog("Signed in " + user.UserId);
                displayName = user.DisplayName ?? "";
                emailAddress = user.Email ?? "";
                photoUrl = user.PhotoUrl ?? "";*/
            }
        }
    }


}
