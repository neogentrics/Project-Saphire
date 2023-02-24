using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    public static ControlsUI Instance { get; private set; }

    // Buttons
    [SerializeField] private Button closeButton;

    // This Section will be created later 
    /*
    // Key Bindings
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;

    // Controller Key Bindings
    [SerializeField] private Button gamepadInteractButton;
    [SerializeField] private Button gamepadInteractAltButton;
    [SerializeField] private Button gamepadPauseButton;
    [SerializeField] private TextMeshProUGUI gamepadInteractText;
    [SerializeField] private TextMeshProUGUI gamepadInteractAltText;
    [SerializeField] private TextMeshProUGUI gamepadPauseText;

    // Rebind Keys to change Controls
    [SerializeField] private Transform pressToRebindKeyTransform;

    */

    private Action onCloseButtonAction;

    private void Start()
    {
        Hide();
        GameManager.Instance.OnGameUnpause += GameManager_OnGameUnpause;

        UpdateVisual();
        //HidePressToRebindKey();
    }

    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() =>
        {
            // Click to change Close Menu
            Hide();
            OptionsMenuUI.Instance.Show();
            onCloseButtonAction();
        });
    }

    private void GameManager_OnGameUnpause(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        // Will Configure this later
        /*
        // Keyboard Bindings:
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);

        // Controller Key Bindings
        gamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamepadInteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);

        */
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

    // Will Configure this later
    /*    
    private void ShowPressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindKey();

        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }*/

}
