using UnityEngine;

namespace VBP.Ball
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] protected float speed;
        protected Vector2 start;
        protected Vector3 end;

        public abstract void NextBall(GameObject nextBall, Vector2 position);
    }
}
