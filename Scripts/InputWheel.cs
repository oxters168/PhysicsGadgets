using UnityEngine;
using UnityHelpers;

namespace PhysicsGadgets
{
    public class InputWheel : MonoBehaviour
    {
        /// <summary>
        /// The pointer that shows the value on the wheel
        /// </summary>
        [Tooltip("The pointer that shows the value on the wheel")]
        public Transform pointer;
        /// <summary>
        /// The bounds of the wheel
        /// </summary>
        [Tooltip("The bounds of the wheel")]
        public Bounds bounds;
        /// <summary>
        /// The radius of the wheel
        /// </summary>
        [Tooltip("The radius of the wheel")]
        public float radius = 1;
        /// <summary>
        /// The output value which represents the angle of the pointer within the wheel (0-1)
        /// </summary>
        [Tooltip("The output value which represents the angle of the pointer within the wheel (0-1)")]
        public float value;

        private Transform interactor;
        private Vector3 prevPosition;

        void Update()
        {
            UpdatePointer();
            CalculateValue();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
            Gizmos.DrawLine(Vector3.zero, (Vector3.right + Vector3.up).normalized * radius);
        }

        public void WheelTriggerStay(TreeCollider.CollisionInfo colInfo)
        {
            SetPointerPosition(transform.InverseTransformPoint(colInfo.collidedWith.transform.position));
        }

        private void CalculateValue()
        {
            float xPos = pointer.localPosition.x;
            float yPos = pointer.localPosition.y;
            Vector2 dir2D = new Vector2(xPos, yPos).normalized;
            float angle = Vector2.SignedAngle(Vector2.right, dir2D);
            if (angle < 0)
                angle += 360;
            value = angle / 360;
        }

        private void UpdatePointer()
        {
            if (interactor != null)
            {
                Vector3 deltaPosition = interactor.position - prevPosition;
                MovePointer(deltaPosition);
            }
        }
        private void MovePointer(Vector3 delta)
        {
            Vector3 nextLocalPosition = pointer.localPosition + delta;
            SetPointerPosition(nextLocalPosition);
        }
        private void SetPointerPosition(Vector3 localPosition)
        {
            float xExtent = bounds.extents.x;
            float yExtent = bounds.extents.y;
            float zExtent = bounds.extents.z;
            localPosition = Vector3.ClampMagnitude(new Vector3(Mathf.Clamp(localPosition.x, -xExtent, xExtent), Mathf.Clamp(localPosition.y, -yExtent, yExtent), Mathf.Clamp(localPosition.z, -zExtent, zExtent)), radius);
            pointer.localPosition = localPosition;
        }
    }
}