using UnityEngine;
using Pospec.Helper.Math;

namespace Pospec.Helper.Characters
{
    public abstract class Topdown2DCharacter : Character2D
    {
        [Header("Movement")]
        [SerializeField, Range(0, 1), Tooltip("0 = fluid; 1 = fixed movement")] private float accelationDamping = 0.3f;
        [SerializeField, Range(0, 1), Tooltip("0 = fluid; 1 = fixed movement")] private float stopDamping = 0.8f;
        [SerializeField, Min(0)] protected float speed = 10;
        [Tooltip("Is character facing right, only used in flipX rotationStyle")]
        [SerializeField] private bool facingRight = true;
        [Tooltip("How should gameObject rotate with direction of character")]
        [SerializeField] private TopdownRotation rotateStyle = TopdownRotation.none;
        protected Vector2 Movement { get; set; }

        protected virtual void Reset()
        {
            if (Rb == null)
                return;

            Rb.gravityScale = 0;
            Rb.freezeRotation = true;
        }

        private void FixedUpdate()
        {
            OnFixedUpdate();
            Move();

            switch (rotateStyle)
            {
                case TopdownRotation.flipX:
                    Flip(Movement.x);
                    break;
                case TopdownRotation.rotateForward:
                    RotateForward(Movement);
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnFixedUpdate() { }

        private void Move()
        {
            Vector2 vel = Rb.velocity;
            Vector2 normVel = Vector2.ClampMagnitude(Movement, 1);
            float decrease = Mathf.Pow(1 - Mathf.Lerp(stopDamping, accelationDamping, Mathf.Abs(normVel.magnitude)), Time.fixedDeltaTime * 10);
            vel *= decrease;
            vel += normVel * (1 - decrease) * speed;
            Rb.velocity = vel;
        }

        private void Flip(float dir)
        {
            if (dir < -0.1f && facingRight || dir > 0.1f && !facingRight)
            {
                facingRight = !facingRight;
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
        }

        private void RotateForward(Vector2 dir)
        {
            if (dir == Vector2.zero)
                return;
            transform.rotation = Quaternion.AngleAxis(dir.AngleInRadians() * Mathf.Rad2Deg, Vector3.forward);
        }
    }

    public enum TopdownRotation { flipX, rotateForward, none }
}
