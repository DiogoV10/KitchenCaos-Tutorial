using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace V10
{
    public class TutorialUI : MonoBehaviour
    {


        [SerializeField] private TextMeshProUGUI keyMoveUpText;
        [SerializeField] private TextMeshProUGUI keyMoveDownText;
        [SerializeField] private TextMeshProUGUI keyMoveLeftText;
        [SerializeField] private TextMeshProUGUI keyMoveRightText;
        [SerializeField] private TextMeshProUGUI keyInteractText;
        [SerializeField] private TextMeshProUGUI keyInteractAlternateText;
        [SerializeField] private TextMeshProUGUI keyPauseText;
        [SerializeField] private TextMeshProUGUI keyGamepadInteractText;
        [SerializeField] private TextMeshProUGUI keyGamepadInteractAlternateText;
        [SerializeField] private TextMeshProUGUI keyGamepadPauseText;


        private void Start()
        {
            GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;
            GameManager.Instance.OnLocalPlayerReadyChanged += GameManager_OnLocalPlayerReadyChanged;
            
            UpdateVisual();

            Show();
        }

        private void GameManager_OnLocalPlayerReadyChanged(object sender, System.EventArgs e)
        {
            if (GameManager.Instance.IsLocalPlayerReady())
            {
                Hide();
            }
        }

        private void GameInput_OnBindingRebind(object sender, System.EventArgs e)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
            keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
            keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
            keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
            keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
            keyInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternate);
            keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
            keyGamepadInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Interact);
            keyGamepadInteractAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_InteractAlternate);
            keyGamepadPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.GamePad_Pause);
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }


    }
}
