using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Components.UI
{
    public class NotationUISingle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _noText;
        [SerializeField] private TextMeshProUGUI _movementText;
        [SerializeField] private Color _primaryColor;
        [SerializeField] private Color _secundaryColor;
        [SerializeField] private Image _bgImage;
        private string firstMove = "";
        private string secondMove = "";

        public void SetNotationText(int number, string firstMovement, string secondMovement = "")
        {
            _noText.text = number.ToString();
            _movementText.text = firstMovement + " " + secondMovement;
        }

        public void SetColor(int i)
        {
            Debug.Log(i);
            if (i == 1)
            {
                _noText.color = _primaryColor;
                _movementText.color = _primaryColor;
                _bgImage.enabled = true;
                _bgImage.color = _secundaryColor;
            }
            else
            {
                _noText.color = _secundaryColor;
                _movementText.color = _secundaryColor;
                _bgImage.enabled = false;
            }
        }
    }
}
