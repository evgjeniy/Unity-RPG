using System;
using System.Collections;
using UnityEngine;
using UnityEngineInternal;

namespace Entities.Enemy
{
    [Serializable]
    public class EnemyController : EntityController
    {
        private Enemy _enemy;

        [SerializeField] private float viewRadius = 10.0f;
        public float ViewRadius => viewRadius;
        public bool IsDead { get; set; }
        
        protected override void Initialize<T1, T2>(Entity<T1, T2> entity)
        {
            _enemy = entity as Enemy;
            IsDead = false;
        }

        //private bool _isHasDestination = false;
        protected override void Update()
        {
            float distance = Vector3.Distance(
                _enemy.transform.position, _enemy.Player.transform.position);

            if (distance <= viewRadius && !IsDead)
            {
                _enemy.State.Gui.gameObject.SetActive(true);
                _enemy.Agent.SetDestination(_enemy.Player.transform.position);
                _enemy.Animator.SetBool("IsMoving", true);
                if (distance <= _enemy.Agent.stoppingDistance)
                {
                    LookAtTarger();
                    _enemy.State.Attack(_enemy.Player);
                    _enemy.Animator.SetBool("IsAttack", true);
                }
                else _enemy.Animator.SetBool("IsAttack", false);

                //_isHasDestination = false;
            }
            else
            {
                _enemy.State.Gui.gameObject.SetActive(false);
                _enemy.Agent.SetDestination(_enemy.transform.position);
                _enemy.Animator.SetBool("IsMoving", false);
            }
            
            /*{
                if (!_isHasDestination)
                {
                    _agent.SetDestination(new Vector3(
                        Random.value, _enemy.transform.position.y,
                        Random.value).normalized * viewRadius);
                    _isHasDestination = true;
                    _animator.SetBool("IsMoving", true);
                }

                if (_isHasDestination && Vector3.Distance(
                        _agent.destination, _enemy.transform.position) <= _agent.stoppingDistance)
                {
                    _isHasDestination = false;
                    _animator.SetBool("IsMoving", false);
                }
            }*/
        }

        public IEnumerator Fall()
        {
            IsDead = true;
            _enemy.GetComponent<CapsuleCollider>().enabled = false;
            _enemy.GetComponent<EnemyInteractable>().enabled = false;
            _enemy.Animator.SetBool("IsDead", true);
            yield return new WaitForSeconds(5);
            _enemy.gameObject.SetActive(false);
        }

        private void LookAtTarger()
        {
            Vector3 direction = (_enemy.Player.transform.position - _enemy.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0.0f, direction.z));
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
    }
}