using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;

namespace Entities.Player
{
    [Serializable]
    public class PlayerController : EntityController
    {
        private Player _player;
        
        [SerializeField] private float turnSmoothTime = 0.1f;
        private Transform _cameraTransform;
        private float _turnSmoothVelocity;
        private Vector3 _moveDirection;

        private float _defaultStepOffset;
        private bool _sprint;
        private bool _jump;
        private bool _attack;

        protected override void Initialize<T1, T2>(Entity<T1, T2> entity)
        {
            _player = entity as Player;
        
            _defaultStepOffset = _player.CharacterController.stepOffset;
            _cameraTransform = Camera.main.transform;
        }

        protected override void Update()
        {
            _sprint = Input.GetKey(KeyCode.LeftShift);
            _jump = Input.GetKeyDown(KeyCode.Space);
            if (!_jump) _attack = Input.GetKeyDown(KeyCode.Mouse0);
            if (!_player.Gui.inventory.IsActive())
                _player.Animator.SetBool("IsAttacking", _attack);

            DefineMoveDirection();
            Jump();
        }

        private void DefineMoveDirection()
        {
            _moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, 
                Input.GetAxisRaw("Vertical")).normalized;

            _player.Animator.SetFloat(
                "InputMagnitude",
                _moveDirection.magnitude / (_sprint ? 1.0f : 2.0f), 
                0.05f, Time.deltaTime);
            
            if (_moveDirection != Vector3.zero)
            {
                _player.Animator.SetBool("IsMoving", true);
                float targetAngle = Mathf.Atan2(_moveDirection.x, _moveDirection.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                _player.transform.rotation = Quaternion.Euler(0.0f,Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, 
                    targetAngle, ref _turnSmoothVelocity, turnSmoothTime), 0.0f);
                _moveDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;
            }
            else _player.Animator.SetBool("IsMoving", false);
        }

        [Header("Jump Settings")]
        [SerializeField] private float jumpSpeed = 5.0f;
        [SerializeField] private float jumpHorizontalSpeed = 5.0f;
        [SerializeField] private float jumpButtonGracePeriod = 0.2f;

        private float _ySpeed;
        private float? _lastGroundedTime;
        private float? _jumpButtonPressedTime;
        private bool _isJumping;
        private bool _isGrounded;
        public float Gravity => _ySpeed;
        public bool Grounded => _isGrounded;

        private void Jump()
        {
            _ySpeed += Physics.gravity.y * Time.deltaTime;
            if (_player.CharacterController.isGrounded) _lastGroundedTime = Time.time;
            if (_jump) _jumpButtonPressedTime = Time.time;

            if (Time.time - _lastGroundedTime <= jumpButtonGracePeriod)
            {
                _player.CharacterController.stepOffset = _defaultStepOffset;
                _ySpeed = -0.5f;
                _player.Animator.SetBool("IsGrounded", true); _isGrounded = true;
                _player.Animator.SetBool("IsJumping", false); _isJumping = false;
                _player.Animator.SetBool("IsFalling", false);
                if (Time.time - _jumpButtonPressedTime <= jumpButtonGracePeriod)
                {
                    _ySpeed = jumpSpeed;
                    _player.Animator.SetBool("IsJumping", true);
                    _isJumping = true;
                    _lastGroundedTime = null;
                    _jumpButtonPressedTime = null;
                }
            }
            else
            {
                _player.CharacterController.stepOffset = 0.0f;
                _player.Animator.SetBool("IsGrounded", false); _isGrounded = false;

                if ((_isJumping && _ySpeed < -0.0f) || _ySpeed < -1)
                    _player.Animator.SetBool("IsFalling", true);
            }

            if (!_isGrounded)
            {
                _moveDirection *= jumpHorizontalSpeed * _moveDirection.magnitude / (_sprint ? 1.0f : 2.0f);
                _moveDirection.y = _ySpeed;
                _player.CharacterController.Move(_moveDirection * Time.deltaTime);
            }
        }

        public IEnumerator Fall()
        {
            _player.Animator.SetBool("IsDead", true);
            yield return new WaitForSeconds(1.66f);
            _player.Gui.deathScreen.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            Time.timeScale = 0.0f;
            Cursor.visible = true;
        }
    }
}