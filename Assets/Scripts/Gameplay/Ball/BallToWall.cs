using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VBP.Ball;

namespace VBP.Ball
{
    public class BallToWall : Ball
    {
        private AudioManager audioManager;
        private void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
            rebound = GameObject.Find("Rebound");
        }

        private void FixedUpdate()
        {
            MoveAndLookAt();
            if (transform.position == rebound.transform.position)
            {
                audioManager.Play("BallBounce");
                NextBall(ballToPlayerZone, transform.position);
            }
        }
        
        #region Move and Look at

        private GameObject rebound;
        private void MoveAndLookAt()
        {
            transform.position = Vector2.MoveTowards(transform.position, rebound.transform.position, speed * Time.deltaTime);
            var dir = rebound.transform.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        #endregion

        #region Add next Ball

        [SerializeField] private GameObject ballToPlayerZone;

        public override void NextBall(GameObject nextBall, Vector2 position)
        {
            Instantiate(nextBall, position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion
    }
}
