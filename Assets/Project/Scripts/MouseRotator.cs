using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class MouseRotator : MonoBehaviour
    {
        [SerializeField] private float sensitivityRotate = 400f;
        [SerializeField] private float inertiaRotate = 16f;
        [SerializeField] private bool isClampAngleX = true;
        [SerializeField] private float clampAngleX = 180f;
        [SerializeField] private bool isClampAngleY = true;
        [SerializeField] private float clambAngleY = 180f;
        [SerializeField] private InputMouseController inputMouseController;

        public Transform TargetTransform { get; set; }
        private Vector3 rotate = Vector3.zero;

        private void OnEnable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnRightMouseButtonEvent.AddListener(OnRightMouseButton);
            }
        }

        private void OnDisable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnRightMouseButtonEvent.RemoveListener(OnRightMouseButton);
            }
        }
        private void OnRightMouseButton(bool isDownButton)
        {
            if (TargetTransform != null)
            {
                if (isDownButton)
                {
                    rotate = TargetTransform.rotation.eulerAngles;
                    inputMouseController.OnMoveMouseEvent.AddListener(OnMoveMouse);
                }
                else
                {
                    inputMouseController.OnMoveMouseEvent.RemoveListener(OnMoveMouse);
                }
            }
        }
        private void OnMoveMouse(Vector2 deltaScreen, Vector3 positionScreen)
        {
            rotate.x += deltaScreen.y * Time.deltaTime * sensitivityRotate;
            rotate.y -= deltaScreen.x * Time.deltaTime * sensitivityRotate;
            if (isClampAngleY) rotate.x = Mathf.Clamp(rotate.x, -clambAngleY, clambAngleY);
            if (isClampAngleX) rotate.y = Mathf.Clamp(rotate.y, -clampAngleX, clampAngleX);
            TargetTransform.rotation = Quaternion.Lerp(TargetTransform.rotation, Quaternion.Euler(rotate), Time.deltaTime * inertiaRotate);
        }
    }
}
