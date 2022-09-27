using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public class Stat
    {
        [SerializeField] private int value;
        private List<int> _modifiers = new List<int>();

        public int Value
        {
            get
            {
                int finalValue = value;
                _modifiers.ForEach(x => finalValue += x);
                return finalValue;
            }
        }

        public void AddModifier(int modifier)
        {
            if (modifier != 0) 
                _modifiers.Add(modifier);
        }

        public void RemoveModifier(int modifier)
        {
            if (modifier != 0)
                _modifiers.Remove(modifier);
        }
    }
}