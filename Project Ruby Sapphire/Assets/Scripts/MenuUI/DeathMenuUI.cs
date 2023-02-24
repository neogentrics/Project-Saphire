using UnityEngine;
using UnityEngine.UI;

public class DeathMenuUI : MonoBehaviour
{
    public static DeathMenuUI Instance { get; private set; }

    // Buttons
    [SerializeField] private Button LoadButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        Hide();
    }


    private void Awake()
    {
        Instance = this;

        LoadButton.onClick.AddListener(() =>
        {
            // Click to change Close Menu & Load new Game
            Hide();
            
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            // Click to Load Main Menu
            Hide();
            
        });
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}


