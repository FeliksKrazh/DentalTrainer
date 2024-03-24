using UnityEngine;
using UnityEngine.UI;

namespace DentalTrainer_FeliksKrazhau
{
    public class InfoObject : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private RectTransform rectTransform;
        public string Text
        {
            get => text.text;
            set => text.text = value;
        }
        public void SetVisible(bool visible)
        {
            rectTransform.gameObject.SetActive(visible);
        }
    }
}
