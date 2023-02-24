using UnityEngine;
using UnityEngine.UI;

public class HUDUI : MonoBehaviour
{
    public static HUDUI Instance { get; private set; }

    // Buttons
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button storeButton;
    [SerializeField] private Button skillsButton;
    [SerializeField] private Button spellsButton;

    private void Start()
    {
        Show();
    }

    // Button Logic

    private void Awake()
    {
        Instance = this;

        inventoryButton.onClick.AddListener(() =>
        {
            // Click to open Inventory
            InventoryUI.Instance.Show();
            Hide();
        }); 
        
        optionsButton.onClick.AddListener(() =>
        {
            // Click to open Options
            OptionsMenuUI.Instance.Show();
            Hide();
        }); 
        
        controlsButton.onClick.AddListener(() =>
        {
            // Click to open the Controls
            ControlsUI.Instance.Show();
            Hide();
        }); 
        
        storeButton.onClick.AddListener(() =>
        {
            // Click to open the Store


            Hide();
        }); 
        
        skillsButton.onClick.AddListener(() =>
        {
            // Click to open Skills Menu


            Hide();
        }); 
        
        spellsButton.onClick.AddListener(() =>
        {
            // Click to open Spells Menu


            Hide();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 1f;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

}
