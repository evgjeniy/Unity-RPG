using Equipments;
using GUI;
using UnityEngine;

namespace Entities.Player
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class Player : Entity<PlayerController, PlayerState>
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private EquipmentController equipmentController;
        
        public GuiHandler Gui { get; set; }
        public Inventory Inventory => inventory;
        public EquipmentController EquipmentController => equipmentController;
        public CharacterController CharacterController { get; private set; }
        public Animator Animator { get; private set; }
        
        protected override void EntityStart()
        {
            CharacterController = GetComponent<CharacterController>();
            Animator = GetComponent<Animator>();
            inventory.Initialize(this);
            equipmentController.Initialize(this);
        }

        protected override void EntityUpdate()
        {
            if (Input.GetKeyDown(KeyCode.U))
                equipmentController.UnequipAll();
        }
        
        public void SetHitAnimation()
        {
            Animator.SetBool("GetHit", true);
            State.Blood.transform.position = transform.position + Vector3.up;
            State.Blood.Play();
        }

        public void ResetHitAnimation()
        {
            Animator.SetBool("GetHit", false);
        }
    
        private void OnAnimatorMove()
        {
            if (Controller.Grounded)
            {
                Vector3 move = Animator.deltaPosition;
                move.y = Controller.Gravity * Time.deltaTime;
                CharacterController.Move(move);
            }
        }
        
        #region Singleton

        public static Player Instance;
        private void Awake()
        {
            if (Instance == null) Instance = this;
        }

        #endregion
    }
}