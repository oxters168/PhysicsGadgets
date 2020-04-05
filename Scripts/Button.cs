using UnityEngine;
using UnityEngine.Events;
using UnityHelpers;

namespace PhysicsGadgets
{
    public class Button : MonoBehaviour
    {
        /// <summary>
        /// The pressable part of the button
        /// </summary>
        [Tooltip("The pressable part of the button")]
        public Transform pressablePart;
        /// <summary>
        /// The local position of the pressable part of the button when it is completely down
        /// </summary>
        [Tooltip("The local position of the pressable part of the button when it is completely down")]
        public Vector3 downPosition;
        /// <summary>
        /// The local position of the pressable part of the button when it is completely up
        /// </summary>
        [Tooltip("The local position of the pressable part of the button when it is completely up")]
        public Vector3 upPosition;
        /// <summary>
        /// The minimum value when the button registers a press
        /// </summary>
        [Range(0, 1), Tooltip("The minimum value when the button registers a press")]
        public float minPressValue = 0.5f;

        /// <summary>
        /// The percent the button is pressed
        /// </summary>
        public float value { get; private set; }
        /// <summary>
        /// If the button has been pressed more than the min press value then this will return true or else false
        /// </summary>
        public bool isPressed { get; private set; }

        [Space(10)]
        public UnityEvent onDown;
        public UnityEvent onUp;

        private void Update()
        {
            Vector3 pressAxis = upPosition - downPosition;
            float totalPressDistance = pressAxis.magnitude;
            Vector3 pressedAmount = pressablePart.localPosition - downPosition;
            float rawDistance = pressedAmount.magnitude;
            float pressedDistance = rawDistance * pressedAmount.PercentDirection(pressAxis);
            value = 1 - Mathf.Clamp01(pressedDistance / totalPressDistance);

            bool wasPressed = isPressed;
            isPressed = value >= minPressValue;
            if (!wasPressed && isPressed)
                onDown?.Invoke();
            else if (wasPressed && !isPressed)
                onUp?.Invoke();
        }
    }
}