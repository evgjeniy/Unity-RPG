using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Entities.Enemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity<EnemyController, EnemyState>
    {
        [Inject] public Player.Player Player { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Animator Animator { get; private set; }
        protected override void EntityStart()
        {
            GetComponent<Rigidbody>().freezeRotation = true;
            Agent = GetComponent<NavMeshAgent>();
            Animator = GetComponent<Animator>();
        }

        protected override void EntityUpdate()
        {
            State.Gui.LookAtPlayerCamera();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Controller.ViewRadius);
        }
    }
}