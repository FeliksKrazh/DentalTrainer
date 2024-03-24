using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class WorldToScreenConvert : MonoBehaviour
    {
        [SerializeField] private WorldToScreenConvertSetting setting;
        [SerializeField] private RectTransform targetRectTransform;
        private Transform targetTransform;
        private Transform cameraTransform;
        private static readonly float HEMICYCLE = 180.0f;
        private float viewAngle = 0;
        private bool isView = false;

        public Transform TargetTransform
        {
            get => targetTransform;
            set => targetTransform = value;
        }

        private void Start()
        {
            cameraTransform = Camera.main.transform;
            viewAngle = HEMICYCLE - setting.AngleView;
            if (targetRectTransform == null)
            {
                targetTransform = this.transform.parent.parent;
            }
        }

        private void Update()
        {
            if (targetTransform != null)
            {
                if (targetRectTransform != null)
                {
                    if (IsInSight == true)
                    {
                        targetRectTransform.position = Camera.main.WorldToScreenPoint(targetTransform.position);
                        if (isView == false)
                        {
                            isView = true;
                            targetRectTransform.gameObject.SetActive(isView);
                        }
                    }
                    else
                    {
                        if (isView == true)
                        {
                            isView = false;
                            targetRectTransform.gameObject.SetActive(isView);
                        }
                    }
                }
            }
        }
        private bool IsInSight
        {
            get
            {
                Vector3 forward = cameraTransform.forward;
                Vector3 dir = targetTransform.position - cameraTransform.position;
                if (Vector3.Dot(forward, dir.normalized) * HEMICYCLE >= viewAngle)
                {
                    if (dir.magnitude <= setting.DistanceView)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else return false;
            }
        }
    }
}