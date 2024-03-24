using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class MouseMover : MonoBehaviour
    {
        [SerializeField] private float inertiaMove = 16f;
        [SerializeField] private InputMouseController inputMouseController;

        public Transform TargetTransform { get; set; }
        private float depth = 0;

        private void OnEnable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnLeftMouseButtonEvent.AddListener(OnLeftMouseButton);
            }
        }

        private void OnDisable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnLeftMouseButtonEvent.RemoveListener(OnLeftMouseButton);
            }
        }

        private void OnLeftMouseButton(bool isDownButton)
        {
            if (TargetTransform != null)
            {
                if (isDownButton)
                {
                    depth = TargetTransform.position.z;
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
            Ray ray = Camera.main.ScreenPointToRay(positionScreen);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, ObjectSelector.DISTANCE_RAY, ObjectSelector.LAYER_MASK))
            {
                if (hit.transform.gameObject.tag == ObjectSelector.TAG_RAY_CAST_PLANE)
                {
                    Vector3 pos = hit.point;
                    pos.z = depth;
                    TargetTransform.position = Vector3.Lerp(TargetTransform.position, pos, inertiaMove * Time.deltaTime);
                }
            }
        }
    }
}
