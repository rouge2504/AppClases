﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NativeAndroidTextShareToParticularApp : MonoBehaviour
{

    public Button shareButton;

    private bool isFocus = false;
    private bool isProcessing = false;

    public string packageName = "com.whatsapp";

    void Start()
    {
        shareButton.onClick.AddListener(ShareText);
    }

    void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    private void ShareText()
    {

#if UNITY_ANDROID
        if (!isProcessing)
        {

            //check if app installed
            if (CheckIfAppInstalled())
            {
                StartCoroutine(ShareTextInAnroid());
            }
            else
            {
                //fallback plan
                //can either disable the whatsapp share button
                //or can a normal share trigger
            }
        }
#else
		Debug.Log("No sharing set up for this platform.");
#endif
    }

    private bool CheckIfAppInstalled()
    {

#if UNITY_ANDROID

        //create a class reference of unity player activity
        AndroidJavaClass unityActivity =
            new AndroidJavaClass("com.unity3d.player.UnityPlayer");

        //get the context of current activity
        AndroidJavaObject context = unityActivity.GetStatic<AndroidJavaObject>("currentActivity");

        //get package manager reference
        AndroidJavaObject packageManager = context.Call<AndroidJavaObject>("getPackageManager");

        //get the list of all the apps installed on the device
        AndroidJavaObject appsList = packageManager.Call<AndroidJavaObject>("getInstalledPackages", 1);

        //get the size of the list for app installed apps
        int size = appsList.Call<int>("size");

        for (int i = 0; i < size; i++)
        {
            AndroidJavaObject appInfo = appsList.Call<AndroidJavaObject>("get", i);
            string packageNew = appInfo.Get<string>("packageName");

            if (packageNew.CompareTo(packageName) == 0)
            {
                return true;
            }
        }

        return false;

#endif
    }

#if UNITY_ANDROID
    public IEnumerator ShareTextInAnroid()
    {

        var shareSubject = "I challenge you to beat my high score in Fire Block";
        var shareMessage = "I challenge you to beat my high score in Fire Block. " +
                           "Get the Fire Block app from the link below. \nCheers\n\n" +
                           "http://onelink.to/fireblock";

        isProcessing = true;

        if (!Application.isEditor)
        {
            //Create intent for action send
            string smsNumber = ""; //Aqui va su numero
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
            ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            //set the package to whatsapp package
            intentObject.Call<AndroidJavaObject>("setPackage", packageName);

            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity =
                unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }

    public IEnumerator ShareTextInAnroidSpecificContact()
    {

        var shareSubject = "I challenge you to beat my high score in Fire Block";
        var shareMessage = "I challenge you to beat my high score in Fire Block. " +
                           "Get the Fire Block app from the link below. \nCheers\n\n" +
                           "http://onelink.to/fireblock";

        isProcessing = true;

        if (!Application.isEditor)
        {
            //Create intent for action send
            string smsNumber = ""; //Aqui va el numero 
            AndroidJavaClass intentClass =
                new AndroidJavaClass("android.content.Intent");
            AndroidJavaObject intentObject =
                new AndroidJavaObject("android.content.Intent");
            intentObject.Call<AndroidJavaObject>
            ("setAction", intentClass.GetStatic<string>("ACTION_SEND"));

            //put text and subject extra
            intentObject.Call<AndroidJavaObject>("setType", "text/plain");
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), shareSubject);
            intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), shareMessage);

            intentObject.Call<AndroidJavaObject>("putExtra", "jid", smsNumber + "@s.whatsapp.net");

            //set the package to whatsapp package
            intentObject.Call<AndroidJavaObject>("setPackage", packageName + ".w4b");

            //call createChooser method of activity class
            AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject currentActivity =
                unity.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject chooser =
                intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, "Share your high score");
            currentActivity.Call("startActivity", chooser);
        }

        yield return new WaitUntil(() => isFocus);
        isProcessing = false;
    }
#elif UNITY_IOS 
    public void ShareTextToIOS(){
        NSString * msg = @"YOUR MSG";
        NSString * urlWhats = [NSString stringWithFormat:@"whatsapp://send?text=%@",msg];
        NSURL * whatsappURL = [NSURL URLWithString:[urlWhats stringByAddingPercentEscapesUsingEncoding:NSUTF8StringEncoding]];
        if ([[UIApplication sharedApplication] canOpenURL: whatsappURL]) {
            [[UIApplication sharedApplication] openURL: whatsappURL];
        } else {
            // Cannot open whatsapp
        }
    }
#endif
}