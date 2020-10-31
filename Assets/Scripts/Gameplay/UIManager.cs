using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VBP.Ball;
using VBP.Player;
using VBP.Puppy;

namespace Gameplay
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private float count;
        [SerializeField] private Text startCount;
        [SerializeField] private PuppyManager puppymanager;
        [SerializeField] private SetBall ball;
        [SerializeField] private Animator sensei;
        [SerializeField] private Animator neko;
        [SerializeField] private PlayerController player;
        private bool startcount = true;
        private void Start()
        {
            startCount.gameObject.SetActive(true);
        }
        private void Update()
        {
            startCount.text = count.ToString("f0");
            if (startcount == true) {
                count -= Time.deltaTime;
                if (count <= 0)
                {
                    startCount.gameObject.SetActive(false); 
                    puppymanager.enabled = true;
                    player.enabled = true;
                    ball.enabled = true;
                    sensei.enabled = true;
                    neko.enabled = true;
                    startcount = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
