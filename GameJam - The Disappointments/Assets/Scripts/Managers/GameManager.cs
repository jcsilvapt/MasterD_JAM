using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Singleton
    public static GameManager instance;

    [Header("LoadScreen")]
    [SerializeField] GameObject loadCanvas;
    [SerializeField] Slider loadSlider;

    [Header("Sound Manager")]
    [SerializeField] AudioMixer audioMixer;

    [Header("UI Manager")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject loseMenu;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject endMenu;
    [SerializeField] Slider s_masterVolume;
    [SerializeField] Slider s_musicVolume;
    [SerializeField] Slider s_sfxVolume;

    private bool isPaused = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            SetSoundsLevel();
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void SetSoundsLevel() {
        float value;
        if (audioMixer.GetFloat("master", out value)) {
            s_masterVolume.value = value;
        }
        if (audioMixer.GetFloat("music", out value)) {
            s_musicVolume.value = value;
        }
        if (audioMixer.GetFloat("sfx", out value)) {
            s_sfxVolume.value = value;
        }
    }

    /// <summary>
    /// Function that gets the current value of the sound mixer.
    /// </summary>
    /// <param name="mixerName">master, music, sfx</param>
    /// <returns></returns>
    private float GetSoundsLevel(string mixerName) {
        float value;
        if (audioMixer.GetFloat(mixerName, out value)) {
            return value;
        }
        return -80f;
    }

    #region LOGIC

    /// <summary>
    /// Function that Pauses the Game in any State
    /// </summary>
    /// <param name="value">True - The game will Pause</param>
    /// <param name="value">False - The game will resume</param>
    private void Pause(bool value) {
        isPaused = value;
        pauseMenu.SetActive(isPaused);
        UI_CloseOptions();
        Time.timeScale = isPaused ? 0 : 1;
    }

    private void PauseByExtra() {
        isPaused = true;
        Time.timeScale = isPaused ? 0 : 1;
    }

    /// <summary>
    /// Function that change the Scene
    /// if AllowLoadScreen is true, there will be a LoadScreen Showing up while loading.
    /// </summary>
    /// <param name="sceneIndex">Index of the scene</param>
    /// <param name="allowLoadScreen">true - Load Screen will pop-up</param>
    private void _ChangeScene(int sceneIndex, bool allowLoadScreen) {
        if (allowLoadScreen) {
            loadCanvas.SetActive(true);
            StartCoroutine(LoadScene(sceneIndex));
        } else {
            if (isPaused) {
                Pause(false);
            }
            SceneManager.LoadScene(sceneIndex);
        }
    }

    private void CloseGame() {
        Application.Quit();
    }

    private void LoadNextLevel() {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        _ChangeScene(currentScene + 1, true);
        winMenu.SetActive(false);
        Pause(false);
    }

    private void PlayerWonLevel() {
        PauseByExtra();
        winMenu.SetActive(true);
    }

    private void PlayerLostLevel() {
        PauseByExtra();
        loseMenu.SetActive(true);
    }

    private void PlayerWon() {
        PauseByExtra();
        endMenu.SetActive(true);
    }

    #endregion

    #region AUDIO

    private void SetMasterVolume(float value) {
        audioMixer.SetFloat("master", value);
    }

    private void SetMusicVolume(float value) {
        audioMixer.SetFloat("music", value);
    }

    private void SetSFXVolume(float value) {
        audioMixer.SetFloat("sfx", value);
    }

    #endregion

    #region WRAPPERS


    public static float GetMixerVolume(string mixerName) {
        if (instance != null) {
            return instance.GetSoundsLevel(mixerName);
        }
        return -80f;
    }


    public static void PlayerLose(bool needAnimation) {
        if (instance != null) {
            if (needAnimation) {
                instance.StartCoroutine(instance.ShowDeadScreen());
            } else {
                instance.PlayerLostLevel();
            }
        }
    }

    public static void PlayerWin() {
        if (instance != null) {
            instance.PlayerWonLevel();
        }
    }

    public static void PlayerWonTheGame() {
        if (instance != null) {
            instance.PlayerWon();
        }
    }

    /// <summary>
    /// Function that Quits the game to Desktop
    /// </summary>
    public static void QuitGame() {
        if (instance != null) {
            instance.CloseGame();
        }
    }

    /// <summary>
    /// Function that Changes the current Scene.
    /// Use allowLoadScreen if the scene you're loading needs PRE-LOAD.
    /// </summary>
    /// <param name="index">ID of the Scene to be loaded</param>
    /// <param name="isToLoad">If True, the loading Screen will appear</param>
    public static void ChangeScene(int sceneIndex, bool allowLoadScreen) {
        if (instance != null) {
            instance._ChangeScene(sceneIndex, allowLoadScreen);
        }
    }

    /// <summary>
    /// Function that Pauses the Game in any State
    /// </summary>
    public static void PauseGame() {
        if (instance != null) {
            instance.Pause(true);
        }
    }

    public static bool IsGamePaused() {
        if (instance != null) {
            return instance.isPaused;
        }
        return false;
    }

    /// <summary>
    /// Function that resumes the Game in any State
    /// </summary>
    public static void ResumeGame() {
        if (instance != null) {
            instance.Pause(false);
        }
    }

    /// <summary>
    /// Function that controls the Master Volume
    /// </summary>
    /// <param name="value">Value of the Master Volume</param>
    public static void SOUND_MasterVolume(float value) {
        if (instance != null) {
            instance.SetMasterVolume(value);
        }
    }

    /// <summary>
    /// Function that controls the Music Volume
    /// </summary>
    /// <param name="value">Value of the Music Volume</param>
    public static void SOUND_MusicVolume(float value) {
        if (instance != null) {
            instance.SetMusicVolume(value);
        }
    }

    /// <summary>
    /// Function that controls the SFX Volume
    /// </summary>
    /// <param name="value">Value of the SFX Volume</param>
    public static void SOUND_SFXVolume(float value) {
        if (instance != null) {
            instance.SetSFXVolume(value);
        }
    }

    #endregion

    #region UI

    public void UI_Resume() {
        Pause(false);
        UI_CloseOptions();
    }

    public void UI_Restart() {
        _ChangeScene(SceneManager.GetActiveScene().buildIndex, false);
        Pause(false);
        UI_CloseOptions();
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
    }

    public void UI_Quit() {
        _ChangeScene(0, false);
        Pause(false);
        UI_CloseOptions();
        loseMenu.SetActive(false);
        winMenu.SetActive(false);
        endMenu.SetActive(false);
    }

    public void UI_LoadNextLevel() {
        LoadNextLevel();
        UI_CloseOptions();
    }

    public void UI_MasterVolume(float value) {
        SetMasterVolume(value);
    }
    public void UI_MusicVolume(float value) {
        SetMusicVolume(value);
    }
    public void UI_SFXVolume(float value) {
        SetSFXVolume(value);
    }

    public void UI_OpenOptions() {
        optionsMenu.SetActive(true);
    }

    public void UI_CloseOptions() {
        optionsMenu.SetActive(false);
    }

    public void UI_ShowEndedMenu() {
        endMenu.SetActive(true);
    }

    #endregion

    #region COROUTINES

    IEnumerator LoadScene(int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone) {

            float progress = Mathf.Clamp01(operation.progress / .9f);

            loadSlider.value = progress;

            yield return null;

        }

        loadSlider.value = 0;
        loadCanvas.SetActive(false);
    }

    IEnumerator ShowDeadScreen() {

        yield return new WaitForSeconds(2f);

        PlayerLostLevel();

    }

    #endregion
}
