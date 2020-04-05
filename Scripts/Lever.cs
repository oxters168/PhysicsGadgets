using UnityEngine;
using UnityEngine.Events;
using UnityHelpers;

namespace PhysicsGadgets
{
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    
    public class Lever : MonoBehaviour
    {
        /// <summary>
        /// The part of the lever that rotates to switch between on and off
        /// </summary>
        [Tooltip("The part of the lever that rotates to switch between on and off")]
        public PhysicsTransform rotatablePart;
        /// <summary>
        /// The axis the rotatable part rotates on
        /// </summary>
        [Tooltip("The axis the rotatable part rotates on")]
        public Vector3 axis = Vector3.right;
        /// <summary>
        /// The degrees of rotation of the rotatable part that represents the on position
        /// </summary>
        [Tooltip("The degrees of rotation of the rotatable part that represents the on position")]
        public float onRotation;
        /// <summary>
        /// The degrees of rotation of the rotatable part that represents the off position
        /// </summary>
        [Tooltip("The degrees of rotation of the rotatable part that represents the off position")]
        public float offRotation;
        /// <summary>
        /// The minimum degrees offset from on or off to snap to that state
        /// </summary>
        [Tooltip("The minimum degrees offset from on or off to snap to that state")]
        public float minOffset;

        public bool isOn { get; private set; }

        [Space(10)]
        public BoolEvent onValueChanged;

        private void Update()
        {
            Vector3 currentEuler = rotatablePart.transform.localRotation.eulerAngles;
            float degreesInAxis = currentEuler.Multiply(axis.normalized).magnitude;
            if (degreesInAxis > 180)
                degreesInAxis -= 360;
            else if (degreesInAxis < -180)
                degreesInAxis += 360;

            float onOffset = Mathf.Abs(degreesInAxis - onRotation);
            float offOffset = Mathf.Abs(degreesInAxis - offRotation);

            if (onOffset <= minOffset)
                rotatablePart.SetAnchorRotation(Quaternion.AngleAxis(onRotation, axis), Space.Self);
            else if (offOffset <= minOffset)
                rotatablePart.SetAnchorRotation(Quaternion.AngleAxis(offRotation, axis), Space.Self);

            bool wasOn = isOn;
            isOn = onOffset > offOffset;
            if (!wasOn && isOn)
                onValueChanged?.Invoke(isOn);
            else if (wasOn && !isOn)
                onValueChanged?.Invoke(isOn);
        }
    }
}