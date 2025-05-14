using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

namespace UniversalOrbitCameraNS
{
    public class UniversalOrbitCamera : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        public Transform Target;

        [SerializeField] private bool workOverUI;

        [Tooltip("Check this box if you want input to be registered even when mouse or finger is over UI element")]
        [Header("Zoom")]
        [SerializeField]
        private float zoomSensitivity = 0.4f;

        [SerializeField] private int zoomMin = 10;
        [SerializeField] private int zoomMax = 100;

        [Header("Rotation/Tilt")] [Tooltip("Leave at zero if you do not want any limits")] [SerializeField]
        private float rotationSensitivity = 0.2f;

        [SerializeField] private Vector2 rotationMinMax;
        [SerializeField] private Vector2 tiltMinMax;

        [Tooltip("Horizontal rotation invert")] [SerializeField]
        private bool invertX;

        [Tooltip("Vertical rotation invert")] [SerializeField]
        private bool invertY;

        [Header("Auto Rotation")] [SerializeField]
        private bool autoRotate;

        [SerializeField] private bool autoRotationSideVertical;

        [Header("Fade")] [SerializeField] private float fadeTime;

        [SerializeField] private Color fadeColor;

        [Header("Technical")] [SerializeField] private float lerpTime = 10;

        private float _alpha = 1;
        private bool _done;

        private bool _fadeOut = true; // fadeOut = false is fadeIn
        private Texture2D _texture;

        private Vector3 newRotation;

        private Transform newTarget;
        private Vector3 newZoom;

        private OrbitCameraControls orbitCameraControls;
        private Vector3 resetRotation;
        private Vector3 resetZoom;

        private int rotInvertX = 1;
        private int rotInvertY = 1;

        private void Awake()
        {
            orbitCameraControls = new OrbitCameraControls();
            ETouch.EnhancedTouchSupport.Enable();
        }

        private void Start()
        {
            newZoom = resetZoom = cameraTransform.localPosition;
            newRotation = resetRotation = transform.rotation.eulerAngles;

            if (invertX) rotInvertX = -1;
            if (invertY) rotInvertY = -1;
        }

        private void Update()
        {
            OrbitCameraInput();
        }

        private void OnEnable()
        {
            orbitCameraControls.Enable();
        }

        private void OnDisable()
        {
            orbitCameraControls.Disable();
        }

        public void OnGUI()
        {
            if (_done) return;
            if (_texture == null) _texture = new Texture2D(1, 1);

            _texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, _alpha));
            _texture.Apply();

            if (_fadeOut)
                _alpha -= Time.deltaTime / fadeTime;
            else
                _alpha += Time.deltaTime / fadeTime;

            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);

            if (_fadeOut && _alpha <= 0)
            {
                _done = true;
            }
            else if (_alpha >= 1)
            {
                if (newTarget != null)
                {
                    Target = newTarget;
                    newTarget = null;
                }

                CameraFade();
            }
        }

        // Change object to orbit, with fade effect
        public void ChangeTarget(Transform transform)
        {
            CameraFade(false);
            newTarget = transform;
        }

        private void OrbitCameraInput()
        {
            // Rotation Mouse
            if (Mouse.current != null && orbitCameraControls.OrbitMap.MouseRotateButton.IsPressed())
                if (workOverUI || !IsOverUI(Mouse.current.position.ReadValue()))
                {
                    var mouseDelta = Mouse.current.delta.ReadValue();
                    newRotation -= new Vector3(0, 1, 0) * mouseDelta.x * rotationSensitivity * rotInvertX;
                    newRotation += new Vector3(1, 0, 0) * mouseDelta.y * rotationSensitivity * rotInvertY;
                }

            // Rotaton Gamepad and Touch
            var rotationDelta = orbitCameraControls.OrbitMap.Rotation.ReadValue<Vector2>();
            if (rotationDelta != Vector2.zero)
            {
                // Mobile camera control only when 1 finger is on the screen
                if (ETouch.Touch.activeTouches.Count == 1)
                {
                    // Touch
                    if (workOverUI || !IsOverUI(ETouch.Touch.activeTouches[0].screenPosition))
                    {
                        newRotation -= new Vector3(0, 1, 0) * rotationDelta.x * rotationSensitivity * rotInvertX;
                        newRotation += new Vector3(1, 0, 0) * rotationDelta.y * rotationSensitivity * rotInvertY;
                    }
                }
                // Zero fingers should be on screen for gamepad control
                else if (ETouch.Touch.activeTouches.Count == 0)
                {
                    // Gamepad
                    newRotation -= new Vector3(0, 1, 0) * rotationDelta.x * rotInvertX;
                    newRotation += new Vector3(1, 0, 0) * rotationDelta.y * rotInvertY;
                }
            }

            // Zoom
            var zoomValue = orbitCameraControls.OrbitMap.Zoom.ReadValue<float>();
            if (zoomValue != 0) newZoom += new Vector3(0, 0, 1) * zoomValue * zoomSensitivity;

            // Touch Zoom
            if (ETouch.Touch.activeTouches.Count == 2)
            {
                var touch = ETouch.Touch.activeTouches[0];
                var touch2 = ETouch.Touch.activeTouches[1];

                // Get touch positions on plane
                var prevPositionTouch = touch.screenPosition - touch.delta;
                var prevPositionTouch2 = touch2.screenPosition - touch2.delta;

                // Get distances
                var initialDistance = Vector2.Distance(prevPositionTouch, prevPositionTouch2);
                var currentDistance = Vector2.Distance(touch.screenPosition, touch2.screenPosition);

                var deltaDistance = initialDistance - currentDistance;

                // Do zoom
                const float mobileZoomSensitivityAdjust = 0.2f;
                newZoom -= new Vector3(0, 0, 1) * deltaDistance * (zoomSensitivity * mobileZoomSensitivityAdjust);
            }

            // Reset Camera
            // if (orbitCameraControls.OrbitMap.Reset.WasPerformedThisFrame())
            // {
            //     newZoom = resetZoom;
            //     newRotation = resetRotation;
            // }

            // Auto Rotation
            if (autoRotate)
            {
                if (autoRotationSideVertical)
                    newRotation += new Vector3(1, 0, 0) * rotationSensitivity;
                else
                    newRotation -= new Vector3(0, 1, 0) * rotationSensitivity;
            }

            // Apply input
            // Position
            transform.position = Vector3.Lerp(
                transform.position,
                Target.position,
                Time.deltaTime * lerpTime
            );

            // Zoom, based on cursor position
            cameraTransform.localPosition = Vector3.Lerp(
                cameraTransform.localPosition,
                ZoomClamp(ref newZoom),
                Time.deltaTime * lerpTime
            );

            // Rotation
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                RotationClamp(ref newRotation),
                Time.deltaTime * lerpTime
            );
        }

        public bool IsOverUI(Vector2 screenPosition)
        {
            if (EventSystem.current != null)
            {
                // Check if over UI element
                var click_results = new List<RaycastResult>();
                var click_data = new PointerEventData(EventSystem.current);
                click_data.position = screenPosition;
                EventSystem.current.RaycastAll(click_data, click_results);

                if (click_results.Count == 0) return false;

                return true;
            }

            return false;
        }

        private Vector3 ZoomClamp(ref Vector3 newZoom)
        {
            newZoom[2] = Mathf.Clamp(newZoom[2], -zoomMax, -zoomMin);
            return newZoom;
        }

        private Quaternion RotationClamp(ref Vector3 newRotation)
        {
            if (tiltMinMax[0] != 0 || tiltMinMax[1] != 0)
                newRotation[0] = Mathf.Clamp(newRotation[0], tiltMinMax[0], tiltMinMax[1]);

            if (rotationMinMax[0] != 0 || rotationMinMax[1] != 0)
                newRotation[1] = Mathf.Clamp(newRotation[1], rotationMinMax[0], rotationMinMax[1]);

            return Quaternion.Euler(newRotation);
        }

        // Fade
        [RuntimeInitializeOnLoadMethod]
        public void CameraFade(bool fadeOut = true)
        {
            _done = false;
            _fadeOut = fadeOut;

            if (fadeOut)
                _alpha = 1;
            else
                _alpha = 0;
        }
    }
}