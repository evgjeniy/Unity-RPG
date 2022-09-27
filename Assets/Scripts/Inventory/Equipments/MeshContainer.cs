using System;
using System.Collections.Generic;
using UnityEngine;

namespace Equipments
{
    [Serializable]
    public class MeshContainer
    {
        public EquipmentSlot equipmentSlot;
        public List<GameObject> defaultMesh = new List<GameObject>();
        public List<GameObject> newMesh = new List<GameObject>();
    }
}