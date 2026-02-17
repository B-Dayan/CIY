using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{
    public GameObject debugMenu;
    public static PlayerControls controls;

    public void Awake()
    {
        controls = new PlayerControls();
        controls.Player.Enable();
        controls.UI.Enable();
        controls.UI.OpenDebugMenu.performed += ToggleDebugMenu;
        controls.UI.Exit.performed += OnExit;
    }

    public void ToggleDebugMenu(InputAction.CallbackContext ctx)
    {
        if (debugMenu.activeSelf)
        {
            debugMenu.SetActive(false);
        }
        else
        {
            debugMenu.SetActive(true);
        }
    }
    
    public static void OnExit(InputAction.CallbackContext ctx)
    {
        OnExit();
    }

    public static void OnExit()
    {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else {
            Application.Quit();
        }
    }
}
