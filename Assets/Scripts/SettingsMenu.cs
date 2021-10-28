using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;


public class SettingsMenu : MonoBehaviour
{
    public AudioSource musicAudio;
    public AudioSource buttonAudio;
    public Dropdown DResolution;

    Resolution[] resolutions;

    public void Start()
    {
        // Regarder toutes les r�solutions disponibles pour l'�cran
        resolutions = Screen.resolutions;

        DResolution.ClearOptions(); // retirer les options pr�cedentes

        List<string> options = new List<string>(); // stocker toutes les r�solutions pour les afficher

        int IndexresolutionACtuelle = 0; // Savoir quelle r�solutions nous avons au d�but

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) 
            {
                IndexresolutionACtuelle = i;
            }
        }

        // Mettre � jour le Dropdown
        DResolution.AddOptions(options);
        DResolution.value = IndexresolutionACtuelle;
        DResolution.RefreshShownValue();
    }

    public void SetResolution(Dropdown dropdown)
    {
        int indexResolution = dropdown.value;
        Resolution resolution = resolutions[indexResolution];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(Slider slider,AudioSource audio)
    {
        float value = slider.value;
        audio.volume = value;
    }

    public void SetMusicVolume(Slider slider)
    {
        SetVolume(slider, musicAudio);
    }
    public void SetButtonVolume(Slider slider)
    {
        SetVolume(slider, buttonAudio);
    }

    public void SetFullScreen(Toggle toggle)
    {
        Screen.fullScreen = toggle.isOn;
    }
}
