using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using VBP.Puppy;

namespace VBP.Puppy
{
    public class PuppyController : MonoBehaviour
    {
        #region Enums State

        public enum State
        {
            Zone,
            Escape,
            Kicked
        };

        [SerializeField]private State actualState;

        public State ActualState
        {
            get => actualState;
            set => actualState = value;
        }

        #endregion

        private PuppyManager puppyManager;
 
        private void Start()
        {
            initialPosition = transform.position;
            gameover = GameObject.Find("GameOver");
            puppyManager = FindObjectOfType<PuppyManager>();
            puppyManager.AddPuppyToList(gameObject);
        }


        private void Update()
        {

        }

        private void FixedUpdate()
        {
            Move();
            if ( transform.position == initialPosition)
            {
                puppyManager.AddPuppyToList(gameObject);
                actualState = State.Zone;
            }
        }

        #region Move

        [SerializeField] private float speed;
        [SerializeField] private float speedp;
        private float t;
        private Vector3 initialPosition;
        private Vector2 startP;
        private Vector2 endP;
        [SerializeField] private GameObject gameover;
        
        private void Move()
        {
            switch (actualState)
            {
                case State.Zone:
                    transform.Translate(Vector2.left * (speed * Time.deltaTime));
                    break;
                case State.Escape:
                    transform.position = Vector2.MoveTowards(transform.position, gameover.transform.position, speed * Time.deltaTime);
                    break;
                case State.Kicked:
                    var x = ((endP.x - startP.x) / 2f) + startP.x;
                    var y = endP.y + 2f;
                    var pmax = new Vector2(x, y);
                    t += Time.deltaTime * speedp;
                    if (t < 1.0f)
                    {
                        transform.position = ParableFunction.Parable(t, startP, pmax, endP);
                    }
                    else
                    {
                        transform.position = endP;
                        t = 1;
                    }
                    break;
            }
        }

        #endregion

        #region Collisions

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("AttackedBall") && actualState == State.Zone)
            {
                puppyManager.RemovePuppyToList(gameObject);
                startP = transform.position;
                endP = initialPosition;
                t = 0;
                actualState = State.Kicked;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Fence"))
            {
                if (other.transform.position.y == initialPosition.y)
                {   
                    puppyManager.RemovePuppyToList(gameObject);
                    actualState = State.Escape;
                }
            }
        }

        #endregion
    }
}
