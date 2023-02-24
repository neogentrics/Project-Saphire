using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public static MainMenuUI Instance { get; private set; }

    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        Instance= this;

        playButton.onClick.AddListener(() =>
        {
            // Click to play game
            Loader.Load(Loader.Scene.StartScene);
        });

        optionsButton.onClick.AddListener(() =>
        {
            // Click to pull up Options
            OptionsMenuUI.Instance.Show();
            
        });

        quitButton.onClick.AddListener(() =>
        {
            // Click to exit game
            Application.Quit();
        });

        Time.timeScale = 1f;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
