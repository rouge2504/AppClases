using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Firebase.Unity.Editor;
using Firebase;
using Firebase.Database;
using Proyecto26;


public class DataBaseManager : MonoBehaviour
{
    public DatabaseReference reference;
    // Start is called before the first frame update
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://appclasse-f518c.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        User user = new User("Rojo", "rouge2504@gmail.com", "Hola como estas");

        RestClient.Put("https://appclasse-f518c.firebaseio.com/"+ user.username +".json", user);
        //RestClient.Put("https://appclasse-f518c.firebaseio.com/"+ user.username +".json", user);

        /*RestClient.Get<User>("https://appclasse-f518c.firebaseio.com/" + user.username + ".json").Then(response =>
        {
            user = response;
        });

        print(user.username);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}

public class User
{
    public string username;
    public string email;
    public string menssage;

    public User()
    {
    }

    public User(string username, string email, string menssage)
    {
        this.username = username;
        this.email = email;
        this.menssage = menssage;
    }
}
