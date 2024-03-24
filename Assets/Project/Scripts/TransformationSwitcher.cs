using System.Collections;
using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    public class TransformationSwitcher : MonoBehaviour
    {
        [SerializeField] private float speedMove = 2.5f;
        [SerializeField] private Transform freeTransform;
        [SerializeField] private Transform parentTransform;
        [SerializeField] private Transform childTransform;

        private bool isParent = true;
        private Vector3 localPos = Vector3.zero;
        private Quaternion localRotate = Quaternion.identity;
        private Coroutine coroutine = null;
        private static readonly float LIM_ZERO = 0.0025f;

        public bool IsParent
        {
            get => isParent;
            set
            {
                if (value)
                {
                    SetParent();
                }
                else
                {
                    SetFree();
                }
            }
        }

        private void Start()
        {
            localPos = childTransform.localPosition;
            localRotate = childTransform.localRotation;
        }
        private void OnDisable()
        {
            StopCoroutine();
        }

        [ContextMenu("Test SetParent")]
        public void SetParent()
        {
            StartCoroutine();
        }

        [ContextMenu("Test SetFree")]
        public void SetFree()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            childTransform.SetParent(freeTransform);
            isParent = false;
        }
        private void StartCoroutine()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            coroutine = StartCoroutine(SubSetParnt());
        }

        private void StopCoroutine()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            childTransform.localPosition = localPos;
            childTransform.localRotation = localRotate;
        }
        private IEnumerator SubSetParnt()
        {
            childTransform.SetParent(parentTransform);
            isParent = true;
            while (Vector3.Magnitude(localPos - childTransform.localPosition) > LIM_ZERO)
            {
                float speedMoveTimeDelta = speedMove * Time.deltaTime;
                childTransform.localPosition = Vector3.Lerp(childTransform.localPosition, localPos, speedMoveTimeDelta);
                childTransform.localRotation = Quaternion.Lerp(childTransform.localRotation, localRotate, speedMoveTimeDelta);
                yield return null;
            }
            StopCoroutine();
        }
    }
}
