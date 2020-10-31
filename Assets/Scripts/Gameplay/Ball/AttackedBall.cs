using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VBP.Player;
using VBP.Puppy;

namespace VBP.Ball
{
    public class AttackedBall : Ball
    {
        private PlayerController playerController;
        private GameObject ballObjective;
        private void Start()
        {
            Time.timeScale = 1f;                                                                            
            start = transform.position;
            end = transform.position;
            puppyManager = FindObjectOfType<PuppyManager>();
            playerController = FindObjectOfType<PlayerController>();
            ballObjective = GameObject.Find("BallObjective");
            playerController.ActualState = PlayerController.ButtonsStates.Nothing;
            ballObjective.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        private void FixedUpdate()
        {
            MoveAndLookAt();
        }

        #region Move and Look at

        private PuppyManager puppyManager;
        private Vector3 objective;

        private void MoveAndLookAt()
        {//6.159745 1.246621
            if (puppyManager.puppys != null)
            {
                objective = puppyManager.AttackedBallObjective().transform.position;
            }
            else
            {
                objective = new Vector3(6.159745f, 1.246621f);
            }
            transform.position = Vector2.MoveTowards(transform.position, objective, speed * Time.deltaTime);
            var dir = objective - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        #endregion

        #region Add next Ball
        
        [SerializeField] private GameObject ballToWall;

        public override void NextBall(GameObject nextBall, Vector2 position)
        {
            Instantiate(nextBall, position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion

        #region Collisions

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Puppy"))
            {
                NextBall(ballToWall, transform.position);
            }
        }

        #endregion
    }
    
}
