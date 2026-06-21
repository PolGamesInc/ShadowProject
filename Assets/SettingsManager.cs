using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public enum MenuState {Control, Sound, Exit}

    [SerializeField] private GameObject Control;
    [SerializeField] private GameObject SliderSound;

    [SerializeField] private Button SoundButton;
    [SerializeField] private Button ControlButton;

    private MenuState currentState;

    private void Start()
    {
        currentState = MenuState.Exit;
        SoundButton.onClick.AddListener(() => SetState(MenuState.Sound));
        ControlButton.onClick.AddListener(() => SetState(MenuState.Control));
    }

   public void LoadMenu(InputAction.CallbackContext context)
    {
        if (context.performed)
            SceneManager.LoadScene(0);
    }
    //Переход на сцену 0 с использованием события

    public void SetState(MenuState newState)
    {
        if(currentState == newState) 
            return;

        currentState = newState;

        Control.SetActive(newState == MenuState.Control);
        SliderSound.SetActive(newState == MenuState.Sound);
    }
    //Переключение с настроек упарвления в настройки звука. Переключение с одного на дргуое. Способ для переключения с одного на другое с возможностью расширения.
}
