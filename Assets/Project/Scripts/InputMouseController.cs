using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DentalTrainer_FeliksKrazhau
{ 
    public class InputMouseController : MonoBehaviour
    {
        public UnityEvent OnStartLeftMouseButtonEvent;
        public UnityEvent OnStartMiddleMouseButtonEvent;
        public UnityEvent OnStartRightMouseButtonEvent;

        public UnityEvent OnFinishLeftMouseButtonEvent;
        public UnityEvent OnFinishMiddleMouseButtonEvent;
        public UnityEvent OnFinishRightMouseButtonEvent;

        public UnityEvent<bool> OnLeftMouseButtonEvent;
        public UnityEvent<bool> OnMiddleMouseButtonEvent;
        public UnityEvent<bool> OnRightMouseButtonEvent;

        public UnityEvent<Vector2, Vector3> OnMoveMouseEvent;
        public UnityEvent<float> OnScrollMouseEvent;

        private Vector2 deltaScreen = Vector2.zero;
        private Vector2 positionScreen = Vector2.zero;

        private bool isDownLeftMouseButton = false;
        private bool isDownMiddleMouseButton = false;
        private bool isDownRightMouseButton = false;

        private Coroutine coroutine = null;

        public Vector2 DeltaScreen
        {
            get => deltaScreen;
            private set => deltaScreen = value;
        }
        public Vector2 PositionScreen
        {
            get => positionScreen;
            private set => positionScreen = value;
        }
        public bool IsDownLeftMouseButton
        {
            get => isDownLeftMouseButton;
            private set
            {
                isDownLeftMouseButton = value;
            }
        }
        public bool IsDownMiddleMouseButton
        {
            get => isDownMiddleMouseButton;
            private set
            {
                isDownMiddleMouseButton = value;
            }
        }
        public bool IsDownRightMouseButton
        {
            get => isDownRightMouseButton;
            private set
            {
                isDownRightMouseButton = value;
            }
        }
        public Vector3 GetPositionMouse
        {
            get
            {
                Vector3 pos = Input.mousePosition;
                return new Vector3(pos.x, pos.y, 0);
            }
        }
        private void StartCoroutine()
        {
            StopCoroutine();
            DeltaScreen = Vector2.zero;
            PositionScreen = GetPositionMouse;
            coroutine = StartCoroutine(SubDeltaMause());
        }
        private void StopCoroutine()
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
            DeltaScreen = Vector2.zero;
            PositionScreen = GetPositionMouse;
            OnMoveMouseEvent?.Invoke(DeltaScreen, PositionScreen);
        }
        private void OnDisable()
        {
            StopCoroutine();
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Input.GetMouseButtonDown((int)KeyMouse.Left))
            {
                if (IsDownLeftMouseButton == false)
                {
                    //print("Left Down");
                    IsDownLeftMouseButton = true;
                    OnStartLeftMouseButtonEvent?.Invoke();
                    OnLeftMouseButtonEvent?.Invoke(IsDownLeftMouseButton);
                    StartCoroutine();
                }
            }
            else
            if (Input.GetMouseButtonUp((int)KeyMouse.Left))
            {
                if (IsDownLeftMouseButton == true)
                { 
                    //print("Left Up");
                    IsDownLeftMouseButton = false;
                    StopCoroutine();
                    OnLeftMouseButtonEvent?.Invoke(IsDownLeftMouseButton);
                    OnFinishLeftMouseButtonEvent?.Invoke();
                }
            }

            if (Input.GetMouseButtonDown((int)KeyMouse.Middle))
            {
                if (IsDownMiddleMouseButton == false)
                {
                    //print("Middle Down");
                    IsDownMiddleMouseButton = true;
                    OnStartMiddleMouseButtonEvent?.Invoke();
                    OnMiddleMouseButtonEvent?.Invoke(IsDownMiddleMouseButton);
                    StartCoroutine();
                }
            }
            else
            if (Input.GetMouseButtonUp((int)KeyMouse.Middle))
            {
                if (IsDownMiddleMouseButton == true)
                {
                    //print("Middle Up");
                    IsDownMiddleMouseButton = false;
                    StopCoroutine();
                    OnMiddleMouseButtonEvent?.Invoke(IsDownMiddleMouseButton);
                    OnFinishMiddleMouseButtonEvent?.Invoke();
                }
            }

            if (Input.GetMouseButtonDown((int)KeyMouse.Right))
            {
                if (IsDownRightMouseButton == false)
                {
                    //print("Right Down");
                    IsDownRightMouseButton = true;
                    OnStartRightMouseButtonEvent?.Invoke();
                    OnRightMouseButtonEvent?.Invoke(IsDownRightMouseButton);
                    StartCoroutine();
                }
            }
            else
            if (Input.GetMouseButtonUp((int)KeyMouse.Right))
            {
                if (IsDownRightMouseButton == true)
                {
                    //print("Right Up");
                    IsDownRightMouseButton = false;
                    StopCoroutine();
                    OnRightMouseButtonEvent?.Invoke(IsDownRightMouseButton);
                    OnFinishRightMouseButtonEvent?.Invoke();
                }
            }
            Vector2 scroll = Input.mouseScrollDelta;
            if (scroll.y != 0)
            {
                OnScrollMouseEvent?.Invoke(scroll.y);
            }
        }

        private IEnumerator SubDeltaMause()
        {
            while (true)
            {
                DeltaScreen = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                PositionScreen = GetPositionMouse;
                OnMoveMouseEvent?.Invoke(DeltaScreen, PositionScreen);
                yield return null;
            }
        }

        public enum KeyMouse
        {
            Left = 0,
            Right = 1,
            Middle = 2
        }
    }
}
