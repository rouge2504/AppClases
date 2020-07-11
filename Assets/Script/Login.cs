using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


using Firebase.Unity.Editor;
using Firebase;
using Firebase.Database;
using Proyecto26;

public class Login : MonoBehaviour
{
    [Header("Registro")]
    public InputField nombre;
    public InputField apellido;
    public InputField mail;
    public InputField password;

    [Header("Login")]
    public InputField mailLogin;
    public InputField passwordLogin;

    public Text failText;

    public bool checkText;

    public string validacion_Mail;
    public string validacion_Password;

    private int validacion;


    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;

    private Usuario _user;

    private bool usuarioNuevoLogeado;

    // Start is called before the first frame update
    void Start()
    {

        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://appclasse-f518c.firebaseio.com/");

        _user = new Usuario("Rogelio", "Trejo", "rouge2504@gmail.com", "admin12345678");

        RestClient.Put("https://appclasse-f518c.firebaseio.com/"+ _user.nombre + ".json", _user);

        
        checkText = false;

        usuarioNuevoLogeado = false;

        InitializeFirebase();

        validacion = PlayerPrefs.GetInt("Validacion");

        string name = PlayerPrefs.GetString("nombre");
        print(name);

        print(validacion);
        if (validacion == 1)
        {
            print("Acceso Concedido");
            SceneManager.LoadScene("Principal");
        }


        /*_user = new Usuario("Rogelio", "Trejo", "rouge2504@gmail.com", "admin12345678");
        _user.Guardar();*/
    }

    // Update is called once per frame
    void Update()
    {
        failText.gameObject.SetActive(checkText);

        ChequeoNuevoUsuario();
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

            usuarioNuevoLogeado = true;



            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
        });
    }

    void ChequeoNuevoUsuario()
    {
        if (usuarioNuevoLogeado)
        {
            _user = new Usuario(nombre.text, apellido.text, mail.text, password.text);
            _user.Guardar();
            usuarioNuevoLogeado = false;
        }
    }

    public void LoginButton()
    {
        if (string.IsNullOrEmpty(mailLogin.text) == false || string.IsNullOrEmpty(passwordLogin.text) == false)
        {
            auth.SignInWithEmailAndPasswordAsync(mailLogin.text, passwordLogin.text).ContinueWith(task => {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted)
                {
                    //Debug.LogError("Encotro un error: " + task.Exception);
                    failText.text = "El mail o el password estan mal";
                    checkText = true;
                    
                    return;
                }

                Firebase.Auth.FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
            });
        }
        else
        {
            failText.text = "Algunos de los campos esta vacio";
            failText.gameObject.SetActive(true);
            //print("Algunos de los campos esta vacio");
        }
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

public class Usuario
{
    public string nombre;
    public string apellido;
    public string mail;
    public string password;


    public Usuario()
    {

    }

    public Usuario(string _name, string _lastName, string _email, string _password)
    {
        nombre = _name;
        apellido = _lastName;
        mail = _email;
        password = _password;

    }

    public void Guardar()
    {
        PlayerPrefs.SetString("nombre", nombre);
        PlayerPrefs.SetString("apellido", apellido);
        PlayerPrefs.SetString("mail", mail);
        PlayerPrefs.SetString("password", password);

        Debug.Log("variables guardadas");
    }
}