using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using VBP.Ball;

namespace VBP.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Vector3 pmax;
        private Animator animator;
        private float d;
        [SerializeField] private AudioManager audioManager;
        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            animator = GetComponent<Animator>();
            d = 0;
        }

        private void Update()
        {
            d -= Time.deltaTime;
            animator.SetFloat("Defending", d);
        }

        private void FixedUpdate()
        {
            if (grounded)
            {
                Move();
                LimitedAreaY();
            }
            LimitedAreaX();
        }

        #region Move
        
        [SerializeField]private float speed;
        private float h;
        private float v;
        [SerializeField] private Joystick joystick;
        [SerializeField] private SpriteRenderer sprite;
        public SpriteRenderer Sprite
        {
            get => sprite;
            set => sprite = value;
        }

        private void Move()
        {
            h = joystick.Horizontal;
            v = joystick.Vertical;
            var movement = new Vector2(h, v);
            transform.Translate(movement * (speed * Time.deltaTime));
            if (h < 0 && Grounded)
            {
                animator.SetBool("Running", true);
                sprite.flipX = true;
            }
            if (h > 0 && Grounded)
            {
                animator.SetBool("Running", true);
                sprite.flipX = false;
            }
            if (h == 0 && grounded)
            {
                animator.SetBool("Running", false);
            }
        }
        
        #endregion

        #region Jump 

        [SerializeField]private float speedJump;
        [SerializeField]private float yJump;
        [SerializeField]private float xJump;
        private float t;
        [SerializeField]private bool grounded;
        public bool Grounded
        {
            get => grounded;
            set => grounded = value;
        }
        private Vector2 startP;
        private Vector3 endP;
        private IEnumerator JumpCoroutine()
        {
            while (grounded == false)
            {
                animator.SetBool("Jumping", true);
                var x = ((endP.x - startP.x) / 2f) + startP.x;
                var y = startP.y + yJump;
                pmax = new Vector2(x, y);
                t += Time.deltaTime * speedJump;
                if (t < 1.0f)
                {
                    transform.position = ParableFunction.Parable(t, startP, pmax, endP);
                }
                else
                {
                    transform.position = endP;
                    t = 1;
                }
                if (transform.position == endP)
                {
                    t = 0;
                    grounded = true;
                    animator.SetBool("Jumping", false);
                    animator.SetBool("Attacking", false);
                    StopCoroutine(nameof(JumpCoroutine));
                }
                yield return new WaitForFixedUpdate();
            }
        }

        #endregion

        #region Buttons

        public enum ButtonsStates
        {
            Jump, Attack, Nothing
        }
        private ButtonsStates actualState;
        public ButtonsStates ActualState
        {
            get => actualState;
            set => actualState = value;
        }
        private bool isHittable;
        public bool IsHittable
        {
            get => isHittable;
            set => isHittable = value;
        }

        [SerializeField] private SetBall setBall;
        [SerializeField] private AttackedBall attackedball;

        public void Jump()
        {
            switch (actualState)
            {
                case ButtonsStates.Jump:
                    if (grounded)
                    {
                        animator.SetBool("Jumping", true);
                        audioManager.Play("ChadJump");
                        startP = transform.position;
                        if (startP.x <= (3.91f - xJump) && startP.x >= (0.345f + xJump))
                        {
                            if (h > 0)
                            {
                                endP = new Vector2(startP.x + xJump, startP.y);
                            }
                            else if (h < 0)
                            {
                                endP = new Vector2(startP.x - xJump, startP.y);
                            }
                            else
                            {
                                endP = startP;
                            }
                        }
                        else if (startP.x > (3.91f - xJump))
                        {
                            if (h > 0)
                            {
                                endP = new Vector2(3.91f, startP.y);
                            }
                            else if (h < 0)
                            {
                                endP = new Vector2(startP.x - xJump, startP.y);
                            }
                            else
                            {
                                endP = startP;
                            }
                        }
                        else if (startP.x < (0.345f + xJump))
                        {
                            if (h > 0)
                            {
                                endP = new Vector2(startP.x + xJump, startP.y);
                            }
                            else if (h < 0)
                            {
                                endP = new Vector2(0.345f, startP.y);
                            }
                            else
                            {
                                endP = startP;
                            }
                        }
                        grounded = false;
                        StartCoroutine(JumpCoroutine());
                    }
                    break;
                case ButtonsStates.Attack:
                    setBall = FindObjectOfType<SetBall>();
                    animator.SetBool("Jumping", false);
                    animator.SetBool("Attacking", true);
                    setBall.NextBall(attackedball.gameObject, setBall.transform.position);
                    audioManager.Play("ChadAttack");
                    break;
                case ButtonsStates.Nothing:
                    setBall = null;
                    break;
            }
        }

        #endregion

        #region Collisions

        [SerializeField]private FeetCollsion feet;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("BallToPlayerZone"))
            {
                if (feet.Onposition)
                {
                    d = 1f;
                    audioManager.Play("ChadDefense");
                }
            }
        }

        #endregion
        
        #region Limited Area
        private void LimitedAreaY()
        {
            if (transform.position.y <= 0.524f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 0.525f), 5f * Time.deltaTime);
            }
            if (transform.position.y >= 2.54f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 2.539f), 5f * Time.deltaTime);
            }
        }
        private void LimitedAreaX()
        {
            if (transform.position.x <= 0.344f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(0.345f, transform.position.y), 5f * Time.deltaTime);
            }
            if (transform.position.x >= 3.911f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(3.91f, transform.position.y), 5f * Time.deltaTime);
            }
        }
        #endregion
    }
}
