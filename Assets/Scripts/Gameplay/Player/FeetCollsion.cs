using System;
using UnityEngine;

namespace VBP.Player
{
    public class FeetCollsion : MonoBehaviour
    {
        [SerializeField] private bool onposition;
        public bool Onposition => onposition;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Shadow"))
            {
                onposition = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Shadow"))
            {
                onposition = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Shadow"))
            {
                onposition = false;
            }
        }
    }
}

