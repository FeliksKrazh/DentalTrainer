using DataUtil;
using UnityEngine;
using UnityEngine.UI;

namespace DentalTrainer_FeliksKrazhau
{
    public class ButtonWorkPanel : MonoBehaviour
    {
        [SerializeField] private FileTransforms fileTransforms;
        [SerializeField] private InfoSystem infoSystem;
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button viewInfoButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            if (fileTransforms != null)
            {
                if (saveButton != null) saveButton.onClick.AddListener(fileTransforms.Save);
                if (loadButton != null) loadButton.onClick.AddListener(fileTransforms.Load);
                if (resetButton != null) resetButton.onClick.AddListener(fileTransforms.ResetToParent);
                if (viewInfoButton != null) viewInfoButton.onClick.AddListener(OnClickViewInfoButton);
                if (quitButton != null) quitButton.onClick.AddListener(OnClickQuitButton);
            }
        }

        private void OnDestroy()
        {
            if (fileTransforms != null)
            {
                if (saveButton != null) saveButton.onClick.RemoveListener(fileTransforms.Save);
                if (loadButton != null) loadButton.onClick.RemoveListener(fileTransforms.Load);
                if (resetButton != null) resetButton.onClick.RemoveListener(fileTransforms.ResetToParent);
                if (viewInfoButton != null) viewInfoButton.onClick.RemoveListener(OnClickViewInfoButton);
                if (quitButton != null) quitButton.onClick.RemoveListener(OnClickQuitButton);
            }
        }

        private void OnClickViewInfoButton()
        {
            if (infoSystem != null)
            {
                infoSystem.IsVisible = !infoSystem.IsVisible;
            }
        }

        private void OnClickQuitButton()
        {
            Application.Quit();
        }
    }
}
