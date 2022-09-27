using System;
using UnityEngine;

namespace Entities
{
    public abstract class Entity<T1, T2> : MonoBehaviour
        where T1 : EntityController where T2 : EntityState
    {
        [SerializeField] private T1 controller; 
        [SerializeField] private T2 state;
        public T1 Controller => controller;
        public T2 State => state;
    
        private Action _onUpdateAction;

        protected abstract void EntityStart();
        protected abstract void EntityUpdate();

        void Start()
        {
            EntityStart();
        
            // initializing the controllers
            controller?.SafeInitialize(ref _onUpdateAction, this);
            state?.Initialize(this);
            state.Blood = Instantiate(
                Resources.Load<GameObject>("BloodSplat_FX"), 
                transform.position + Vector3.down * 100, 
                Quaternion.Euler(0.0f, 0.0f, 0.0f)).GetComponent<ParticleSystem>();
        }

        void Update()
        {
            EntityUpdate();
            
            // controller update call
            _onUpdateAction?.Invoke();
            state.AttackCooldown -= Time.deltaTime;
        }
    }
}