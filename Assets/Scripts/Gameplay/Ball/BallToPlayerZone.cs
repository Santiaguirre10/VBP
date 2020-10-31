using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using VBP.Player;
using Random = UnityEngine.Random;

namespace VBP.Ball
{
    public class BallToPlayerZone : Ball
    {
        private AudioManager audioManager;
        void Start()
        {
            ballObjective = GameObject.Find("BallObjective");
            ballObjective.GetComponentInChildren<SpriteRenderer>().enabled = true;
            audioManager = FindObjectOfType<AudioManager>();
            feet = FindObjectOfType<FeetCollsion>();
            start = transform.position;
            var x = Random.Range(1.3f, 3.5f);
            var y = Random.Range(0.6f, 2.174f);
            end = new Vector2(x, y);
            ballObjective.transform.position = end;
        }
        void Update()
        {
        
        }
        private void FixedUpdate()
        {
            MoveTo();
        }

        #region MoveTo

        private float t;
        private GameObject ballObjective;
        [SerializeField] private GameObject setBall;
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
                if (count >= 3)
                {
                    NextBall(setBall, new Vector2(2.386f, 3.265f));
                }
            }
        }

        #endregion

        #region Collisions

        private FeetCollsion feet;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (feet.Onposition)
                {
                    NextBall(defendedBall, transform.position);  
                }
            }
        }

        #endregion

        #region Add next Ball

        [SerializeField] private GameObject defendedBall;

        public override void NextBall(GameObject nextBall, Vector2 position)
        {
            Instantiate(nextBall, position, Quaternion.identity);
            Destroy(gameObject);
        }
        
        #endregion
    }
}
