using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace V10
{
    public class GamePauseUI : MonoBehaviour
    {


        [SerializeField] private Button resumeButton;
        [SerializeField] private Button mainMenuButton;
        [SerializeField] private Button optionsButton;


        private void Awake()
        {
            resumeButton.onClick.AddListener(() =>
            {
                GameManager.Instance.TogglePauseGame();
            });

            mainMenuButton.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.MainMenuScene);
            });

            optionsButton.onClick.AddListener(() =>
            {
                Hide();
                OptionsUI.Instance.Show(Show);
            });
        }

        private void Start()
        {
            GameManager.Instance.OnLocalGamePaused += GameManager_OnLocalGamePaused;
            GameManager.Instance.OnLocalGameUnPaused += GameManager_OnLocalGameUnPaused;

            Hide();
        }

        private void GameManager_OnLocalGameUnPaused(object sender, System.EventArgs e)
        {
            Hide();
        }

        private void GameManager_OnLocalGamePaused(object sender, System.EventArgs e)
        {
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);

            resumeButton.Select();
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }


    }
}
