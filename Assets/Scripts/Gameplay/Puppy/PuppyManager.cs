using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VBP.Puppy
{
    public class PuppyManager : MonoBehaviour
    {
        [SerializeField] private float puppystimer;
        
        private void Start()
        {
            
        }
        private void Update()
        {
            AttackedBallObjective();
        }
        private void OnEnable()
        {
            InvokeRepeating(nameof(PuppySpammer), 0, puppystimer);
        }
        
        #region ObjectivePuppy

        private float minX = 10f;
        public List<GameObject> puppys;
        [SerializeField]private GameObject objectivePuppy;

        public GameObject AttackedBallObjective()
        {
            if (puppys.Count == 0)
            {
                return objectivePuppy = null;
            }
            else
            {
                foreach (var t in puppys)
                {
                    if (minX > t.transform.position.x)
                    {
                        minX = t.transform.position.x;
                    }
                }
                foreach (var t in puppys)
                {
                    if (minX == t.transform.position.x)
                    {
                        objectivePuppy = t;
                    }
                }
                return objectivePuppy;
            }
        }

        #endregion

        #region Add or Remove Puppy to list

        public void AddPuppyToList(GameObject puppy)
        {
            puppys.Add(puppy);
            minX = 10f;
        }

        public void RemovePuppyToList(GameObject puppy)
        {
            puppys.Remove(puppy);
            minX = 10f;
        }

        #endregion

        #region Puppy spammer

        [SerializeField] private Transform[] puppyspamer;
        [SerializeField] private GameObject[] puppytype;
        private void PuppySpammer()
        {
            var rdm = Random.Range(0, 4);
            var rdmp = Random.Range(0, 4);
            Instantiate(puppytype[rdmp], puppyspamer[rdm].transform.position, Quaternion.identity);
        }

        #endregion
        
    }
}
