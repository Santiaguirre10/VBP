
using System;
using Gameplay;
using UnityEngine;
using VBP.Player;
using VBP.Puppy;
using Random = UnityEngine.Random;

namespace VBP.Ball
{
    public class SetBall : Ball
    {
        private PuppyManager puppyManager;
        private AudioManager audioManager;
        private void Start()
        {
            ballObjective = GameObject.Find("BallObjective");
            audioManager = FindObjectOfType<AudioManager>();
            playerController = FindObjectOfType<PlayerController>();
            puppyManager = FindObjectOfType<PuppyManager>();
            feet = FindObjectOfType<FeetCollsion>();
            start = transform.position;
            var x = Random.Range(0.345f, 3.91f);
            var y = Random.Range(0.525f, 2.539f);
            end = new Vector3(x, y);
            ballObjective.transform.position = end;
            ballObjective.GetComponentInChildren<SpriteRenderer>().enabled = true;
            playerController.ActualState = PlayerController.ButtonsStates.Jump;
        }

        private void Update()
        {
            
        }

        private void FixedUpdate()
        {
            MoveTo();
        }

        #region Move
        private float t;
        private GameObject ballObjective;
        private PlayerController playerController;
        private FeetCollsion feet;
        private float count;
        private void MoveTo()
        {
            var x = ((end.x - start.x) / 2f) + start.x;
            var y = end.y + 4f;
            var pmax = new Vector2(x, y);
            var rateVelocity = 1f / Vector2.Distance(start, end) * speed;
            t += Time.deltaTime * rateVelocity;
            if (t < 1.0f)
            {
                transform.position = ParableFunction.Parable(t, start, pmax, end);
            }
            else
            {
                transform.position = end;
                t = 1;
                audioManager.Play("BallBounce");
                count += Time.deltaTime;
                if (count>=3)
                {
                    NextBall(gameObject, start);
                }
            }
        }

        #endregion

        #region Add next Ball

        [SerializeField] private GameObject attackedBall;
        public override void NextBall(GameObject nextBall, Vector2 position)
        {
            Instantiate(nextBall, position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion

        #region Collisions

        [SerializeField] private SpriteRenderer sprite;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (t > 0.79f && t < 0.93f)
                {
                    if (other.CompareTag("Player"))
                    {
                        if (t > 0.79f && t < 0.93f)
                        {
                            if (!playerController.Grounded)
                            {
                                playerController.ActualState = PlayerController.ButtonsStates.Attack;
                                playerController.Sprite.flipX = false;
                                sprite.color = Color.red;
                            }
                            else
                            {
                                sprite.color = Color.white;
                            }
                        }
                        else
                        {
                            sprite.color = Color.white;
                        }
                    }
                }
                else
                {
                    playerController.ActualState = PlayerController.ButtonsStates.Jump;
                }
            }
        }
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (t > 0.79f && t < 0.93f)
                {
                    if (!playerController.Grounded)
                    {
                        playerController.ActualState = PlayerController.ButtonsStates.Attack;
                        playerController.Sprite.flipX = false;
                        sprite.color = Color.red;
                    }
                    else
                    {
                        sprite.color = Color.white;
                    }
                }
                else
                {
                    sprite.color = Color.white;
                }
            }
            else
            {
                playerController.ActualState = PlayerController.ButtonsStates.Jump;
            }
        }

        #endregion
    }
}
