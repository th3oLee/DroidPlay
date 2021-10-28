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

}
