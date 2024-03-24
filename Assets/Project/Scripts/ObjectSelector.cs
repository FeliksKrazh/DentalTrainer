using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class ObjectSelector : MonoBehaviour
    {
        [SerializeField] private InputMouseController inputMouseController;
        [SerializeField] private MouseMover mouseMover;
        [SerializeField] private MouseRotator mouseRotator;

        public static readonly float DISTANCE_RAY = 25f;
        public static readonly string TAG_RAY_CAST_PLANE = "RayCastPlane";
        public static readonly int LAYER_MASK = 7; // ObjectControll

        private WorldToScreenConvert worldToScreenConvert = null;
        private void OnEnable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnStartLeftMouseButtonEvent.AddListener(OnStartLeftOrRightMouseButton);
                inputMouseController.OnStartRightMouseButtonEvent.AddListener(OnStartLeftOrRightMouseButton);
                inputMouseController.OnStartMiddleMouseButtonEvent.AddListener(OnStartMiddleMouseButton);
                inputMouseController.OnFinishLeftMouseButtonEvent.AddListener(OnFinishMouseButton);
                inputMouseController.OnFinishRightMouseButtonEvent.AddListener(OnFinishMouseButton);
                inputMouseController.OnFinishMiddleMouseButtonEvent.AddListener(OnFinishMouseButton);
            }
        }

        private void OnDisable()
        {
            if (inputMouseController != null)
            {
                inputMouseController.OnStartLeftMouseButtonEvent.RemoveListener(OnStartLeftOrRightMouseButton);
                inputMouseController.OnStartRightMouseButtonEvent.RemoveListener(OnStartLeftOrRightMouseButton);
                inputMouseController.OnStartMiddleMouseButtonEvent.AddListener(OnStartMiddleMouseButton);
                inputMouseController.OnFinishLeftMouseButtonEvent.RemoveListener(OnFinishMouseButton);
                inputMouseController.OnFinishRightMouseButtonEvent.RemoveListener(OnFinishMouseButton);
                inputMouseController.OnFinishMiddleMouseButtonEvent.RemoveListener(OnFinishMouseButton);
            }
        }

        private Transform GetToRayTransform
        {
            get
            {
                Ray ray = Camera.main.ScreenPointToRay(inputMouseController.GetPositionMouse);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, DISTANCE_RAY))
                {
                    if (hit.transform.gameObject.tag != TAG_RAY_CAST_PLANE && hit.transform.gameObject.layer == LAYER_MASK)
                    {
                        return hit.transform;
                    }
                }
                return null;
            }
        }
        private void SetTargetTransform(Transform target)
        {
            if (mouseMover != null)
            {
                mouseMover.TargetTransform = target;
            }
            if (mouseRotator != null)
            {
                mouseRotator.TargetTransform = target;
            }
        }
        private void OnStartLeftOrRightMouseButton()
        {
            Transform toRayTransform = GetToRayTransform;
            if (toRayTransform != null)
            {
                TransformationSwitcher transformationSwitcher = toRayTransform.GetComponent<TransformationSwitcher>();
                if (transformationSwitcher != null)
                {
                    transformationSwitcher.SetFree();
                }
                SetTargetTransform(toRayTransform);
            }
        }
        private void OnStartMiddleMouseButton()
        {
            Transform toRayTransform = GetToRayTransform;
            if (toRayTransform != null)
            {
                TransformationSwitcher transformationSwitcher = toRayTransform.GetComponent<TransformationSwitcher>();
                if (transformationSwitcher != null)
                {
                    transformationSwitcher.SetParent();
                }
            }
        }
        private void OnFinishMouseButton()
        {
            SetTargetTransform(null);
        }
    }
}
