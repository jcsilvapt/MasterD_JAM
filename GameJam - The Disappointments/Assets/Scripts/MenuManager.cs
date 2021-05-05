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
    [SerializeField] private Animator animatorLevelSelectBackButton;

    private void Start()
    {
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
                animatorLevelSelectBackButton.SetBool("Show", false);
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
