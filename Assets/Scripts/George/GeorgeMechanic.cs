using System;
using System.Collections;
using UnityEngine;

    public class GeorgeMechanic : MonoBehaviour
    {
        //0 Means there is nothing there
        //1 Means that there is something dry
        //2 Means that there is something wet
        //3 Means that there is something grown
    
        private Animator _animator;
        public LayerMask isBush;
        public bool isWet = false;
        public int numberOfClones = 0;
        public int maxClones;
        public bool startAnimation = false;
        public inControl inControl;
        public bool isOnFire;
        public bool isGrowing = false;
        //public inControl inControl;

        public GameObject George;
        public GameObject WaterBushReaction;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            startAnimation = false;
            if ((isWet) && numberOfClones <= maxClones)
            {
                startAnimation = true;
                isWet = false;
                numberOfClones++;
                StartCoroutine(SpawnGeorgeClone());
                if(!isNotOnTop())
                {
                    _animator.Play("Grow");
                }
            }
            else
            {
                startAnimation = false;
            }

            if (isOnFire == true)
            {
                _animator.Play("Burn");
                StartCoroutine(DestroyBushAfterWait());
            }
        }
        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.CompareTag("WaterSpirit") && !isWet && numberOfClones < maxClones && !isGrowing)
            { ;
                WaterBushReaction = Instantiate(WaterBushReaction);
                var position = coll.transform.position;
                WaterBushReaction.transform.position = new Vector3(position.x, position.y);
                Destroy(coll.gameObject);
                isGrowing = true;
                connectionScript connectionScript = coll.gameObject.GetComponent<connectionScript>();
                inControl.numbersToSkip.Add(connectionScript.controlledInt);
                isWet = true;
                //if(coll.controlledInt)
                inControl.controlled = 0;
                inControl._current = 0;
                
            }
            else if (coll.gameObject.CompareTag("FireSpirit"))
            {
                isOnFire = true;
            }
        }
        
        private bool isNotOnTop()
        { Vector2 size = new Vector2(0.4f, 0.1f);
            Vector2 castOrigin = transform.position + new Vector3(0f, 1.2f, 0f);
            RaycastHit2D hit = Physics2D.BoxCast(castOrigin, size, 0f, Vector2.down, 0f, isBush);
            return hit.collider != null; }

        IEnumerator SpawnGeorgeClone()
        {
            yield return new WaitForSeconds (.6f);
            WaterBushReaction.transform.position = new Vector2(-100, -100);
            GameObject GeorgeClone = Instantiate(George);
            var position = transform.position;
            GeorgeClone.transform.position = new Vector3(position.x, position.y + numberOfClones,position.z + numberOfClones);
            isGrowing = false;
            
        }
        
        IEnumerator DestroyBushAfterWait()
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
