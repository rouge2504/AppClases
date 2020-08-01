using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using UnityEngine.Video;

using UnityEngine.Networking;


using UnityEngine.UI;

public class Example : MonoBehaviour
{
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private string status;

    public RawImage img;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cam = GameObject.Find("Main Camera");
        videoPlayer = cam.AddComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "[ThePruld] When you go dark souls with your best mates.mp4");

        videoPlayer.isLooping = true;
        videoPlayer.Pause();
        status = "Press to play";


        StartCoroutine(CargarTextura());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CargarTextura()
    {
        string url = Path.Combine(Application.streamingAssetsPath, "102322345_2800957556698998_7359553755936718848_o.jpg");
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();
        byte[] textureBytes = unityWebRequest.downloadHandler.data;
        Texture2D texture2D = new Texture2D(960, 960, TextureFormat.PVRTC_RGBA4, false);
        texture2D.LoadImage(textureBytes);
        img.texture = texture2D;
    }

    IEnumerator Temporizador(float tiempo)
    {
        print("Empezando a contar");
        yield return new WaitForSeconds(tiempo);
        print("Pasaron 3 segundos");
        yield return new WaitForSeconds(tiempo);
        print("Pasaron  6 segundos");
        yield return new WaitForSeconds(tiempo);
        print("Termino de contar");
    }

    void OnGUI()
    {
        GUIStyle buttonWidth = new GUIStyle(GUI.skin.GetStyle("button"));
        buttonWidth.fontSize = 18 * (Screen.width / 800);

        if (GUI.Button(new Rect(Screen.width / 16, Screen.height / 16, Screen.width / 3, Screen.height / 8), status, buttonWidth))
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
                status = "Press to play";
            }
            else
            {
                videoPlayer.Play();
                status = "Press to pause";
            }
        }
    }
}
