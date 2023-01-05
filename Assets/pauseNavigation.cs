using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class pauseNavigation : MonoBehaviour
{
    public PlayerControls playerControls;
    public PlayerMovement playerMovement;
    private InputAction pause, fire, resetLevel;
    private bool isPaused;

    public GameObject pauseMenu;

    CursorLockMode desiredMode;

    private void Awake()
    {
        playerControls = new PlayerControls();
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += Pause;

        resetLevel = playerControls.Player.ResetLevel;
        resetLevel.Enable();
        resetLevel.performed += ResetLevel;
    }

    private void OnDisable()
    {
        pause.Disable();
        resetLevel.Disable();
    }

    private void Pause(InputAction.CallbackContext context)
    {
        
        ReturnToGame();
    }

    private void ResetLevel(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        playerMovement.enabled = true;
        Cursor.visible = false;
        desiredMode = CursorLockMode.Confined;
    }

    public void ReturnToGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            playerMovement.enabled = true;
            Cursor.visible = false;
            desiredMode = CursorLockMode.Confined;

        }
        else
        {
            Time.timeScale = 0;
            playerMovement.enabled = false;
            Cursor.visible = true;
            desiredMode = CursorLockMode.None;
        }
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
