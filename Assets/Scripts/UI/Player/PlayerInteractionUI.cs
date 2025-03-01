using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInteractionUI : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _messageText;

        public void UpdateUI(Interactable interactable)
        {
            if (interactable != null)
            {
                _messageText.text = interactable.GetInteractionMessage();
                _canvasGroup.alpha = 1f;
            }
            else
            {
                _canvasGroup.alpha = 0f;
            }
        }
    }
}