using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using VBP.Puppy;
using Random = UnityEngine.Random;

namespace VBP.Ball
{
    public class DefendedBall : Ball
    {
        private GameObject ballObjective;
        private GameObject sensei;
        private Animator senseiAnimator;
        private AudioManager audioManager;
        [SerializeField] private PuppyManager puppyManager;
        
        
        // Start is called before the first frame update
        void Start()
        {
            start = transform.position;
            end = new Vector3(2.386f, 3.265f);
            sensei = GameObject.Find("Sensei");
            senseiAnimator = sensei.GetComponent<Animator>();
            audioManager = FindObjectOfType<AudioManager>();
            puppyManager = FindObjectOfType<PuppyManager>();
            ballObjective = GameObject.Find("BallObjective");
            ballObjective.GetComponentInChildren<SpriteRenderer>().enabled = false;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (transform.position == end)
            {
                if (puppyManager.puppys.Count > 0)
                {
                    NextBall(setBall, end);  
                }
            }
            if (t > 0.9f & t < 1f)
            {
                senseiAnimator.SetBool("Setting", true);
                audioManager.Play("SenseiSet");
            }
            else
            {
                senseiAnimator.SetBool("Setting", false);
            }
        }

        private void FixedUpdate()
        {
            MoveTo();
        }

        #region MoveTo

        [SerializeField]private float t;
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
            }
        }
        
        #endregion

        #region Add next ball

        [SerializeField] private GameObject setBall;
        private Animator _animator;

        public override void NextBall(GameObject nextBall, Vector2 position)
        {
            Instantiate(nextBall, position, Quaternion.identity);
            Destroy(gameObject);
        }

        #endregion
    }
    
}
