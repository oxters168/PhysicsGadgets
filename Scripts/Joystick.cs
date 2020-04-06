using UnityEngine;
using UnityHelpers;

namespace PhysicsGadgets
{
    public class Joystick : MonoBehaviour
    {
        public ConfigurableJoint stick;

        [Space(10), Range(-1, 1)]
        public float horizontal;
        [Range(-1, 1)]
        public float vertical;

        [Space(10)]
        public Vector3 horizontalAxis = Vector3.forward;
        public float maxHorizontalAngle = 30;
        public float horizontalDeadzone = 5;

        [Space(10)]
        public Vector3 verticalAxis = Vector3.right;
        public float maxVerticalAngle = 30;
        public float verticalDeadzone = 5;

        private void FixedUpdate()
        {
            float degreesHorizontal = stick.transform.localRotation.eulerAngles.Multiply(horizontalAxis).magnitude;
            if (degreesHorizontal > 180)
                degreesHorizontal -= 360;
            else if (degreesHorizontal < -180)
                degreesHorizontal += 360;
            degreesHorizontal *= -1;

            float degreesVertical = stick.transform.localRotation.eulerAngles.Multiply(verticalAxis).magnitude;
            if (degreesVertical > 180)
                degreesVertical -= 360;
            else if (degreesVertical < -180)
                degreesVertical += 360;

            float sign = Mathf.Sign(degreesHorizontal);
            horizontal = sign * (Mathf.Clamp(Mathf.Abs(degreesHorizontal), horizontalDeadzone, maxHorizontalAngle) - horizontalDeadzone) / (maxHorizontalAngle - horizontalDeadzone);

            sign = Mathf.Sign(degreesVertical);
            vertical = sign * (Mathf.Clamp(Mathf.Abs(degreesVertical), verticalDeadzone, maxVerticalAngle) - verticalDeadzone) / (maxVerticalAngle - horizontalDeadzone);

        }
    }
}