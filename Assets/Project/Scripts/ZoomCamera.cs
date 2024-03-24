using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class ZoomCamera : MonoBehaviour
    {
        [SerializeField] private float freeDistance = 5f;
        [SerializeField] private float sensitivityMove = 0.05f;
        [SerializeField] private Transform TargetTransform;
        [SerializeField] private InputMouseController inputMouseController;

        private void OnEnable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnScrollMouseEvent.AddListener(OnScrollMouse);
            }
        }

        private void OnDisable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnScrollMouseEvent.RemoveListener(OnScrollMouse);
            }
        }

        private void OnScrollMouse(float scroll)
        {
            if (TargetTransform != null)
            {
                Vector3 pos = TargetTransform.position;
                pos += TargetTransform.forward * scroll * sensitivityMove;
                TargetTransform.position = Vector3.ClampMagnitude(pos, freeDistance);
            }
        }
    }
}
