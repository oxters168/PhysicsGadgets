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

        public bool isOn;
        private bool currentlyOn;
        private bool reached;

        [Space(10)]
        public BoolEvent onValueChanged;

        private void Start()
        {
            currentlyOn = isOn;
            onValueChanged?.Invoke(currentlyOn);
        }
        private void Update()
        {
            bool leverOnOn = GetLeverIsOnOn();

            bool wasOn = currentlyOn;

            if (reached)
            {
                if (isOn != currentlyOn)
                {
                    currentlyOn = isOn;
                    reached = false;
                }
                else
                {
                    currentlyOn = leverOnOn;
                    isOn = leverOnOn;
                }
            }

            //We do it this way because we don't want to set reached to false freely
            if (leverOnOn == currentlyOn)
                reached = true;

            SetLeverRotation(currentlyOn);

            if (!wasOn && currentlyOn)
                onValueChanged?.Invoke(currentlyOn);
            else if (wasOn && !currentlyOn)
                onValueChanged?.Invoke(currentlyOn);
        }

        private void SetLeverRotation(bool onOff)
        {
            rotatablePart.SetAnchorRotation(GetRotation(onOff), Space.Self);
        }

        public bool GetLeverIsOnOn()
        {
            float degreesInAxis = Vector3.SignedAngle(transform.up, rotatablePart.transform.up, axis);
            float onOffset = Mathf.Abs(degreesInAxis - GetOnAngle());
            float offOffset = Mathf.Abs(degreesInAxis - GetOffAngle());
            return onOffset < offOffset; //I'm an idiot (or was since it's working now)
        }
        private Quaternion GetRotation(bool onOff)
        {
            if (onOff)
                return Quaternion.AngleAxis(GetOnAngle(), axis);
            else
                return Quaternion.AngleAxis(GetOffAngle(), axis);
        }

        private float GetOnAngle()
        {
            return onRotation;
        }
        private float GetOffAngle()
        {
            return offRotation;
        }
    }
}