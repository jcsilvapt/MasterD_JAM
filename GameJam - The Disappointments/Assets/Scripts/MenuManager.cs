using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Controlling Menu Flow
    private enum Menu
    {
        TitleScreen,
        MainMenu,
        LevelSelect,
        OptionsMenu,
        CreditsMenu
    }

    private Menu currentMenu;

    //Menu GameObjects
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelect;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;

    //Main Menu Animator References
    [SerializeField] private Animator animatorMainMenuPlayButton;
    [SerializeField] private Animator animatorMainMenuOptionsButton;
    [SerializeField] private Animator animatorMainMenuCreditsButton;
    [SerializeField] private Animator animatorMainMenuQuitButton;

    //Level Select Menu Animator References
    [SerializeField] private Animator animatorLevelSelectLeftButton;
    [SerializeField] private Animator animatorLevelSelectLevelButton;
    [SerializeField] private Animator animatorLevelSelectRightButton;
    [SerializeField] private Animator animatorLevelSelectLevelPreview;
    [SerializeField] private Animator animatorLevelSelectBackButton;

    //Options Menu Animator References
    [SerializeField] private Animator animatorOptionsMenuMasterVolumeBackground;
    [SerializeField] private Animator animatorOptionsMenuMasterVolumeSlider;
    [SerializeField] private Animator animatorOptionsMenuMusicVolumeBackground;
    [SerializeField] private Animator animatorOptionsMenuMusicVolumeSlider;
    [SerializeField] private Animator animatorOptionsMenuSFXVolumeBackground;
    [SerializeField] private Animator animatorOptionsMenuSFXVolumeSlider;
    [SerializeField] private Animator animatorOptionsMenuBackButton;

    //Credits Menu Animator References
    [SerializeField] private Animator animatorCreditsMenuMadeBy;
    [SerializeField] private Animator animatorCreditsMenuFabio;
    [SerializeField] private Animator animatorCreditsMenuJorge;
    [SerializeField] private Animator animatorCreditsMenuBackButton;

    //Level Selected Text Reference
    [SerializeField] private Text levelSelectText;

    //Current Level Selected
    private int currentLevel;

    private void Start()
    {
        currentLevel = 1;

        currentMenu = Menu.TitleScreen;
    }

    private void Update()
    {
        if (currentMenu == Menu.TitleScreen && Input.anyKeyDown)
        {
            currentMenu = Menu.MainMenu;
            HideTitleScreen();
            ShowMainMenu();
        }
    }

    #region Menu Methods

    #region Title Screen
    private void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
    }

    private void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
    #endregion

    #region Main Menu
    private void ShowMainMenu()
    {
        mainMenu.SetActive(true);
    }

    private void HideMainMenu()
    {
        mainMenu.SetActive(false);
    }
    #endregion

    #region Level Select
    private void ShowLevelSelect()
    {
        levelSelect.SetActive(true);
    }

    private void HideLevelSelect()
    {
        levelSelect.SetActive(false);
    }
    #endregion

    #region Options Menu
    private void ShowOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    private void HideOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }
    #endregion

    #region Credits Menu
    private void ShowCreditsMenu()
    {
        creditsMenu.SetActive(true);
    }

    private void HideCreditsMenu()
    {
        creditsMenu.SetActive(false);
    }
    #endregion

    #endregion

    #region Button Methods

    #region Main Menu
    public void PlayButton()
    {
        currentMenu = Menu.LevelSelect;
        HideMainMenuButtons();
    }

    public void OptionsButton()
    {
        currentMenu = Menu.OptionsMenu;
        HideMainMenuButtons();
    }

    public void CreditsButton()
    {
        currentMenu = Menu.CreditsMenu;
        HideMainMenuButtons();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    #endregion

    #region Back Button
    public void BackButton()
    {
        HideCurrentMenuButtons();
        currentMenu = Menu.MainMenu;
    }

    #endregion

    #region Level Select
    public void PreviousButton()
    {
        if(currentLevel <= 1)
        {
            currentLevel = 4;
        }
        else
        {
            currentLevel -= 1;
        }

        levelSelectText.text = "Level " + currentLevel.ToString();
    }

    public void NextButton()
    {
        if (currentLevel >= 4)
        {
            currentLevel = 1;
        }
        else
        {
            currentLevel += 1;
        }

        levelSelectText.text = "Level " + currentLevel.ToString();
    }

    public void StartLevel()
    {
        GameManager.ChangeScene(currentLevel, false);
    }
    #endregion

    #endregion

    #region Animator Methods
    private void HideMainMenuButtons()
    {
        animatorMainMenuPlayButton.SetBool("Show", false);
        animatorMainMenuOptionsButton.SetBool("Show", false);
        animatorMainMenuCreditsButton.SetBool("Show", false);
        animatorMainMenuQuitButton.SetBool("Show", false);
    }

    private void HideCurrentMenuButtons()
    {
        switch (currentMenu) {
            case Menu.LevelSelect:
                animatorLevelSelectLeftButton.SetBool("Show", false);
                animatorLevelSelectLevelButton.SetBool("Show", false);
                animatorLevelSelectRightButton.SetBool("Show", false);
                animatorLevelSelectLevelPreview.SetBool("Show", false);
                animatorLevelSelectBackButton.SetBool("Show", false);
                break;
            case Menu.OptionsMenu:
                animatorOptionsMenuMasterVolumeBackground.SetBool("Show", false);
                animatorOptionsMenuMasterVolumeSlider.SetBool("Show", false);
                animatorOptionsMenuMusicVolumeBackground.SetBool("Show", false);
                animatorOptionsMenuMusicVolumeSlider.SetBool("Show", false);
                animatorOptionsMenuSFXVolumeBackground.SetBool("Show", false);
                animatorOptionsMenuSFXVolumeSlider.SetBool("Show", false);
                animatorOptionsMenuBackButton.SetBool("Show", false);
                break;
            case Menu.CreditsMenu:
                animatorCreditsMenuMadeBy.SetBool("Show", false);
                animatorCreditsMenuFabio.SetBool("Show", false);
                animatorCreditsMenuJorge.SetBool("Show", false);
                animatorCreditsMenuBackButton.SetBool("Show", false);
                break;
        }
    }
    #endregion

    #region Animation Events
    public void AnimationEnded()
    {
        switch (currentMenu)
        {
            case Menu.MainMenu:
                ShowMainMenu();
                HideTitleScreen();
                HideLevelSelect();
                HideOptionsMenu();
                HideCreditsMenu();
                break;
            case Menu.LevelSelect:
                ShowLevelSelect();
                HideTitleScreen();
                HideMainMenu();
                HideOptionsMenu();
                HideCreditsMenu();
                break;
            case Menu.OptionsMenu:
                ShowOptionsMenu();
                HideTitleScreen();
                HideMainMenu();
                HideLevelSelect();
                HideCreditsMenu();
                break;
            case Menu.CreditsMenu:
                ShowCreditsMenu();
                HideTitleScreen();
                HideMainMenu();
                HideLevelSelect();
                HideOptionsMenu();
                break;
        }
    }
    #endregion
}
