using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance { get; private set; }

    // Buttons
    [SerializeField] private Button closeButton;
    [SerializeField] private Button controlsMenuButton;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    // Logic
    private string Master = "Master";
    private string Music = "Music";
    private string Audio = "Audio";
    Resolution[] resolutions;

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() =>
        {
            // Click to change Close Options
            Hide();            
        });

        controlsMenuButton.onClick.AddListener(() =>
        {
            // Click to open The Controls Menu
            Hide();
            ControlsUI.Instance.Show();
        });
    }

    private void Start()
    {        
        Hide();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    private void SetVolume(float volume)
    {
        audioMixer.SetFloat(Master, volume);
        audioMixer.SetFloat(Music, volume);
        audioMixer.SetFloat(Audio, volume);
    }
    private void SetAudio(float volume)
    {
        audioMixer.SetFloat(Audio, volume);
    }
    private void SetMusic(float volume)
    {
        audioMixer.SetFloat(Music, volume);
    }
    private void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    private void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Show()
    {        
        gameObject.SetActive(true);
        closeButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
