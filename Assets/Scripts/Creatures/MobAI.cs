using System;
using System.Collections;
using Components;
using Creatures.CreaturesStateMachine;
using PlayerFolder.PlayerParticles;
using UnityEngine;

namespace Creatures
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] private LayerCheck vision;
        [SerializeField] private LayerCheck canAttack;

        [SerializeField] private float alarmDelay = 1f;
        [SerializeField] private float attackCooldown = 1f;
        
        private CreatureParticleEvent _creatureParticleEvent;

        private Coroutine _current;
        private GameObject _target;
        
        private Creature _creatureOld;

        private void Awake()
        {
            _creatureOld = GetComponent<Creature>();
            _creatureParticleEvent = GetComponentInChildren<CreatureParticleEvent>();
        }
        
        private void Start()
        {
            StartState(Patrolling());
        }
        
        public void OnHeroInVision(GameObject go)
        {
            /*if (_creatureOld.IsDead) return;
            _target = go;
            StartState(AgroToHero());*/
        }

        private IEnumerator AgroToHero()
        {
            _creatureParticleEvent.HandleExclamationParticle(); // 
            yield return new WaitForSeconds(alarmDelay);
            StartState(GoToHero());
        }

        private IEnumerator GoToHero()
        {
            
            while (vision.IsTouchingLayer)
            {
                if (canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    SetDirectionToTarget();
                }
                
                yield return null;
            }
        }

        private IEnumerator Attack()
        {
            while (canAttack.IsTouchingLayer)
            {
                
                _creatureOld.SetDirection(0);
                //_creatureOld.Attack();
                yield return new WaitForSeconds(attackCooldown);
            }
            
            StartState(GoToHero());
        }

        private void SetDirectionToTarget()
        {
            var direction = (_target.transform.position - transform.position).normalized; // вектор направления к герою
            direction.y = 0;
            _creatureOld.SetDirection(direction.x);
        }

        private IEnumerator Patrolling()
        {
            yield return null;
        }

        private void StartState(IEnumerator coroutine)
        {
            
            if (_current != null)
            {
                StopCoroutine(_current);
            }

            _current = StartCoroutine(coroutine);
        }
    }
    
}