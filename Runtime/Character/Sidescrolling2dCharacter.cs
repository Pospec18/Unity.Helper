using System.Collections;
using UnityEngine;

namespace Pospec.Helper.Character
{
    public abstract class Sidescrolling2dCharacter : Character
    {
        [Header("Movement")]
        [SerializeField, Range(0, 1), Tooltip("0 = fluid; 1 = fixed movement")] private float accelationDamping = 0.3f;
        [SerializeField, Range(0, 1), Tooltip("0 = fluid; 1 = fixed movement")] private float stopDamping = 0.8f;
        [SerializeField, Min(0)] private float speed = 10;
        [SerializeField, Min(0)] private float maxFallingSpeed = 15;
        [SerializeField] private bool facingRight = true;

        [Header("Jump")]
        [Tooltip("Shape for ground check")]
        [SerializeField] private RayShape groundCheck;
        [Tooltip("Layers player could jump from")]
        [SerializeField] private LayerMask groundMask = 8;
        [Tooltip("Duration from start to the highest position")]
        [SerializeField, Min(0)] private float jumpDuration = 0.4f;
        [Tooltip("Max jump height in units")]
        [SerializeField, Min(0)] private float jumpHeight = 5;
        [Tooltip("Multiplies the character's gravity when falling while jumping")]
        [SerializeField, Range(1, 5)] private float jumpDownGravityMultiplier = 1.5f;
        [Tooltip("Adds downward force when the jump button is released before the end of the jump")]
        [SerializeField, Range(0, 10)] private float jumpStopCutoff = 6;
        [Tooltip("The time after leaving the edge that the character can still jump")]
        [SerializeField, Range(0, 1)] private float coyoteTime = 0.1f;
        [Tooltip("The time before arriving on the ground, where if character inputs jump, the jump will be performed upon landing")]
        [SerializeField, Range(0, 1)] private float bufferJump = 0.2f;

        /// <summary>Is used as input for the movement</summary>
        protected float HorizVelocity { get; set; }
        /// <summary>Is used as input fot the jump</summary>
        protected bool JumpInput { get; set; }
        /// <summary>Is character is on jumpable collider</summary>
        protected bool OnGround { get; private set; }

        private bool prevOnGround = false;
        private bool jumping = false;
        private bool coyoteJump = false;

        protected override void Start()
        {
            base.Start();
            Rb.freezeRotation = true;
            Rb.gravityScale = 1 / (2 * jumpDuration * jumpDuration);
        }

        protected virtual void Reset()
        {
            if (Rb == null)
                return;

            Rb.freezeRotation = true;
        }

        protected virtual void FixedUpdate()
        {
            Move();
            OnGround = groundCheck.IsOverlaping(groundMask);
            if (OnGround != prevOnGround)
            {
                if (!OnGround)
                    StartCoroutine(CoyoteJumpTimer());
                prevOnGround = OnGround;
            }
            Vector2 vel = Rb.velocity;
            if (-vel.y > maxFallingSpeed)
            {
                vel.y = -maxFallingSpeed;
                Rb.velocity = vel;
            }
            Flip(HorizVelocity);
        }

        private void Move()
        {
            Vector2 vel = Rb.velocity;
            float normHorVel = Mathf.Clamp(HorizVelocity, -1, 1);
            float decrease = Mathf.Pow(1 - Mathf.Lerp(stopDamping, accelationDamping, Mathf.Abs(normHorVel)), Time.fixedDeltaTime * 10);
            vel.x *= decrease;
            vel.x += normHorVel * (1 - decrease) * speed;
            Rb.velocity = vel;
        }

        protected bool TryJump()
        {
            if (!JumpInput)
                return false;

            if (!OnGround && !coyoteJump)
            {
                StartCoroutine(JumpBufferTimer());
                return false;
            }

            if (jumping)
                return false;

            StartCoroutine(Jump());
            return true;
        }

        private IEnumerator Jump()
        {
            jumping = true;
            Vector2 vel = Rb.velocity;
            Rb.gravityScale = 1 / (2 * jumpDuration * jumpDuration);
            vel.y = Mathf.Sqrt(2 * Rb.gravityScale * -Physics2D.gravity.y * jumpHeight);
            Rb.velocity = vel;

            float t = 0;
            while (JumpInput && t < jumpDuration && Rb.velocity.y > 0)
            {
                t += Time.deltaTime;
                yield return null;
            }

            if (t < jumpDuration && !JumpInput)
            {
                vel = Rb.velocity;
                vel.y -= jumpStopCutoff * (1 - (t / jumpDuration));
                Rb.velocity = vel;
            }

            Rb.gravityScale *= jumpDownGravityMultiplier;
            while (!OnGround)
                yield return null;
            Rb.gravityScale /= jumpDownGravityMultiplier;
            jumping = false;
        }

        private IEnumerator CoyoteJumpTimer()
        {
            coyoteJump = true;
            yield return Helper.GetWait(coyoteTime);
            coyoteJump = false;
        }

        private IEnumerator JumpBufferTimer()
        {
            float t = 0;
            while (t < bufferJump)
            {
                t += Time.deltaTime;
                yield return null;
                if (OnGround && !jumping)
                    StartCoroutine(Jump());
            }
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

        protected virtual void OnDisable()
        {
            jumping = false;
            coyoteJump = false;
        }
    }
}
