using TMPro;
using UnityEngine;

namespace Game.Components.UI
{
    public class NotationUISingle : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _noText;
        [SerializeField] private TextMeshProUGUI _movementText;
        private string firstMove = "";
        private string secondMove = "";

        public void SetNotationText(int number, string firstMovement, string secondMovement = "")
        {
            _noText.text = number.ToString();
            _movementText.text = firstMovement + " " + secondMovement;
        }
    }
}
