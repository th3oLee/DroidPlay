<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class MenuManager : MonoBehaviour
{

    public Text adresseIP;
    private string previousIP;

    public Dropdown DResolution;

    public void Update()
    {
        /*
        string hostName = Dns.GetHostName();
        IPHostEntry host = Dns.GetHostEntry(hostName);
        string IP = "";
        // Code inspiré de StackOverflow pour savoir comment r�cup�r� l'adresse IPV4 local.
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IP = ip.ToString();
            }
        }
        if (IP !=previousIP)
        {
            previousIP = IP;
            adresseIP.text = "Adresse IP : " + IP;
        }*/
    }
    public void DisablePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }

    public void EnablePanel (GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ApplySettings()
    {
        switch (DResolution.value)
        {
            case 0:
                Screen.SetResolution(720,480,true);
                break;
            case 1:
                Screen.SetResolution(1280,720,true);
                break;
            case 2:
                Screen.SetResolution(1440,900,true);
                break;
            case 3:
                Screen.SetResolution(1080,1920,true);
                break;
        }
    }

    public void playSound(AudioSource audio)
    {
        audio.PlayOneShot(audio.clip);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;

public class MenuManager : MonoBehaviour
{

    public Text adresseIP;
    private string previousIP;

    public void Update()
    {
        string hostName = Dns.GetHostName();
        IPHostEntry host = Dns.GetHostEntry(hostName);
        string IP = "";
        // Code inspiré de StackOverflow pour savoir comment récupéré l'adresse IPV4 local.
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                IP = ip.ToString();
            }
        }
        if (IP !=previousIP)
        {
            previousIP = IP;
            adresseIP.text = "Adresse IP : " + IP;
        }
    }
    public void DisablePanel(GameObject Panel)
    {
        Panel.SetActive(false);
    }

    public void EnablePanel (GameObject Panel)
    {
        Panel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void playSound(AudioSource audio)
    {
        audio.PlayOneShot(audio.clip);
    }

    public void LoadLabyrintheGame()
    {
        // A completer pour charger la scène du labyrinthe et l'afficher et passé le téléphone en paramètres
    }


}
>>>>>>> Stashed changes
