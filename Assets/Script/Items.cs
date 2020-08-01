using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Proyecto26;

using System;

using System.Xml;
using System.Xml.Serialization;
using UnityEngine.Networking;

using UnityEngine.UI;

public class Items : MonoBehaviour
{
    public RawImage img;
    public GameObject contenido;
    public GameObject contenidoAClonar;

    Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {

        //CreadorDeContenido();
        DesplegadoDeContenido();
    }

    void DesplegadoDeContenido()
    {
        string path = "Assets/Resources/Lovecraft.json";
        string json = File.ReadAllText(path);
        print(json);
        Archives[] archives = JsonAyundante.FromJson<Archives>(json);
        string url2 = Application.streamingAssetsPath +archives[0].urlImagen;
        print(url2);
        StartCoroutine(CargarTextura2(url2));
        for (int i = 0; i < archives.Length; i++)
        {
            GameObject clon = Instantiate(contenidoAClonar);
            clon.transform.parent = contenido.transform;
            Contenido content = clon.GetComponent<Contenido>();
            string url = Path.Combine(Application.streamingAssetsPath, archives[i].urlImagen);
            StartCoroutine(CargarTextura(url));
            content.titulo.text = archives[i].nombre;
            content.clasificacion.text = archives[i].clasificacion;
        }
        contenidoAClonar.SetActive(false);
    }

    IEnumerator CargarTextura2(string urlImage)
    {
        string url = Path.Combine(Application.streamingAssetsPath, urlImage);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        byte[] textureBytes = unityWebRequest.downloadHandler.data;
        Texture2D texture2D = new Texture2D(480, 480, TextureFormat.PVRTC_RGBA4, false);
        texture2D.LoadImage(textureBytes);
        img.texture = texture2D;
    }

    IEnumerator CargarTextura(string urlImage)
    {
        string url = Path.Combine(Application.streamingAssetsPath, urlImage);
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        byte[] textureBytes = unityWebRequest.downloadHandler.data;
        Texture2D texture2D = new Texture2D(960, 960, TextureFormat.PVRTC_RGBA4, false);
        texture2D.LoadImage(textureBytes);
        texture = texture2D;
    }

    void CreadorDeContenido()
    {
        string path = "Assets/Resources/Lovecraft.json";

        Archives[] archives = new Archives[4];

        archives[0] = new Archives("Perros de Tindalos", "Tindalos.jpg", "Raza extraterrestre", "Carnívoro", "Los ángulos del tejido espacio-temporal", "Desconocido: son criaturas inteligentes, pero es difícil determinar hasta qué punto", "Se organizan en manadas", "Garras, lengua y toxinas");
        archives[1] = new Archives("Colores surgidos del Espacio", "Colorespacio.jpg", "Raza extraterrestre", "Consume energía vital", "El espacio, pero puede proliferar en entornos terrestres", "Desconocido", "No tienen organización social", "Toxinas");
        archives[2] = new Archives("Dholes", "Dholes.jpg", "Monstruos de las Tierras del Sueño.", "", "Montañas y subsuelo de las Tierras del Sueño. También pueden sobrevivir fuera del espacio angular", "Desconocido. Se sabe que pueden cumplir órdenes sencillas", "Desconocido", "Fauces y baba corrosiva");
        archives[3] = new Archives("Shoggoth", "Shoggoth.jpg", "Criaturas autóctonas ancestrales", "Fundamentalmente carnívoros", "Sobre todo lechos marinos, pero se adaptan con facilidad a entornos terrestres", "Infrahumana", "Desconocido", "Tentáculos y fauces");
        string json = JsonAyundante.ToJson<Archives>(archives, true);

        File.WriteAllText(path, json);
    }

}

[Serializable]
public class Archives
{
    public string nombre;
    public string urlImagen;
    public string clasificacion;
    public string alimentacion;
    public string habitad;
    public string gradoDeInteligencia;
    public string sistemaSocial;
    public string armas;

    public Archives()
    {

    }

    public Archives(string nombre, string urlImagen, string clasificacion, string alimentacion, string habitad, string gradoDeInteligencia, string sistemaSocial, string armas)
    {
        this.nombre = nombre;
        this.urlImagen = Path.Combine(Application.streamingAssetsPath, "/" + nombre + "/" + urlImagen); ;
        this.clasificacion = clasificacion;
        this.alimentacion = alimentacion;
        this.habitad = habitad;
        this.gradoDeInteligencia = gradoDeInteligencia;
        this.sistemaSocial = sistemaSocial;
        this.armas = armas;
    }
}


public static class JsonAyundante
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
