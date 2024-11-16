using TMPro;
using UnityEngine;

namespace Scripts.View
{
    public class TextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void UpdateText(string text) =>
            _text.text = text;
    }
}