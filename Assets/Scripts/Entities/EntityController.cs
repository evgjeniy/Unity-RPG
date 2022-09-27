using System;

namespace Entities
{
    public abstract class EntityController
    {
        public void SafeInitialize<T1, T2>(ref Action updateAction, Entity<T1, T2> entity)
            where T1 : EntityController where T2 : EntityState
        {
            updateAction += Update;
            Initialize(entity);
        }

        protected abstract void Initialize<T1, T2>(Entity<T1, T2> entity)
            where T1 : EntityController where T2 : EntityState;

        protected abstract void Update();
    }
}