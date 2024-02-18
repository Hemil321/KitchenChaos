using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsVolumeText;
    [SerializeField] private TextMeshProUGUI musicvolumeText;

    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAltButton;
    [SerializeField] private Button pauseButton;

    [SerializeField] private TextMeshProUGUI moveUpButtonText; 
    [SerializeField] private TextMeshProUGUI moveDownButtonText;
    [SerializeField] private TextMeshProUGUI moveRightButtonText;
    [SerializeField] private TextMeshProUGUI moveLeftButtonText;
    [SerializeField] private TextMeshProUGUI interactButtonText;
    [SerializeField] private TextMeshProUGUI interactAltButtonText;
    [SerializeField] private TextMeshProUGUI pauseButtonText;

    [SerializeField] private Slider soundEffectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;

    [SerializeField] private Transform pressToRebindTransform;

    private Action onCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        closeButton.onClick.AddListener(() =>
        {
            onCloseButtonAction();
            Hide();
        });

        soundEffectsVolumeSlider.onValueChanged.AddListener((float soundEffectsVolume) =>
        {
            SoundManager.Instance.SetVolume(soundEffectsVolume);
            UpdateVisual();
        });

        musicVolumeSlider.onValueChanged.AddListener((float musicVolume) =>
        {
            MusicManager.Instance.SetVolume(musicVolume);
            UpdateVisual();
        });

        moveUpButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact); });
        interactAltButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { RebindBinding(GameInput.Binding.Pause); });
    }

    private void Start()
    {
        soundEffectsVolumeSlider.value = SoundManager.Instance.GetVolume();
        musicVolumeSlider.value = MusicManager.Instance.GetVolume();

        Hide();
        HidePressToRebindWindow();
        UpdateVisual();
        
        KitchenGameManager.Instance.OnGameUnpaused += KitchenGameManager_OnGameUnpaused;
    }

    private void KitchenGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsVolumeText.text = Mathf.Round(soundEffectsVolumeSlider.value * 100).ToString();

        musicvolumeText.text = Mathf.Round(musicVolumeSlider.value * 100).ToString();

        moveUpButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Move_Up);

        moveDownButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Move_Down);

        moveLeftButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Move_Left);

        moveRightButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Move_Right);

        interactButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Interact);

        interactAltButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Interact_Alternate);

        pauseButtonText.text = GameInput.Instance.GetBinding(GameInput.Binding.Pause);
    }

    public void Show(Action onCloseButtonAction)
    {
        this.onCloseButtonAction = onCloseButtonAction;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindWindow()
    {
        pressToRebindTransform.gameObject.SetActive(true);
    }

    private void HidePressToRebindWindow()
    {
        pressToRebindTransform.gameObject.SetActive(false);
    }

    private void RebindBinding(GameInput.Binding binding)
    {
        ShowPressToRebindWindow();

        //Here, we are passing a delegate function to the rebindBinding function of the game input
        //To pass multiple functions to the same delegate, we use lambda expression
        GameInput.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindWindow();
            UpdateVisual();
        });
    }
}
