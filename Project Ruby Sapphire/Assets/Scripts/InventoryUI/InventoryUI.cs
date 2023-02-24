using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    // Buttons
    [SerializeField] private Button closeButton;
    [SerializeField] private Button equiptButton;
    [SerializeField] private Button removeButton;

    private void Start()
    {
        Hide();
    }

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() =>
        {
            // Click to change Close Menu
            Hide();
        });
        equiptButton.onClick.AddListener(() =>
        {
            // Click to Equipt Item

        });
        removeButton.onClick.AddListener(() =>
        {
            // Click to Remove Item

        });
    }

    public void Show()
    {
        gameObject.SetActive(true);

        closeButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
