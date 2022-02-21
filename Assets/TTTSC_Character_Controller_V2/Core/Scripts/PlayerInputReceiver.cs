using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TTTSC_Character_Controller_V2.Core.Scripts
{
    [RequireComponent(typeof(CharacterFST))]
    [RequireComponent(typeof (CharacterConfig))]
    public class PlayerInputReceiver : MonoBehaviour
    {
        private float _lookX, _lookY;

        private CharacterFST _characterFST;
    
        public event Action<Vector2, bool> walkInputEvent, lookInputEvent;
        public event Action<float> sprintInputEvent, crouchInputEvent, jumpInputEvent;


        public PlayerInputSender playerInputEvents;

        private void OnEnable()
        {
            playerInputEvents = new PlayerInputSender();
            _characterFST = GetComponent<CharacterFST>();
        
            playerInputEvents.Enable();
            playerInputEvents.Controlls.Walk.performed += WalkInputReceiver;
            playerInputEvents.Controlls.LookX.performed += LookXInputReceiver;
            playerInputEvents.Controlls.LookY.performed += LookYInputReceiver;
            playerInputEvents.Controlls.Sprint.performed += SprintInputReceiver;
            playerInputEvents.Controlls.Jump.performed += JumpInputReceiver;
            playerInputEvents.Controlls.Crouch.performed += CrouchInputReceiver;
        }

        private void OnDisable()
        {
            playerInputEvents.Disable();
            playerInputEvents.Controlls.Walk.performed -= WalkInputReceiver;
            playerInputEvents.Controlls.LookX.performed -= LookXInputReceiver;
            playerInputEvents.Controlls.Sprint.performed -= SprintInputReceiver;
            playerInputEvents.Controlls.Jump.performed -= JumpInputReceiver;
            playerInputEvents.Controlls.Crouch.performed -= CrouchInputReceiver;
        }

        private void LookXInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();
            bool performing = value != 0;

            _lookX = value;
            Look(performing);
        }

        private void LookYInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<float>();
            bool performing = value != 0;

            _lookY = value;
            Look(performing);
        }

        private void Look(bool performing)
        {
            var look = new Vector2(_lookX, _lookY);

            lookInputEvent?.Invoke(look, performing);
        }

        private void WalkInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();

            bool performing = !(value == new Vector2(0,0));

            walkInputEvent?.Invoke(value, performing);
        }

        private void SprintInputReceiver(InputAction.CallbackContext ctx)
        {
            if(_characterFST.movementType != CharacterFST.MovementType.Crouch & GetComponent<CharacterConfig>().allowSprint == true)
                _characterFST.movementType = CharacterFST.MovementType.Sprint;

            if (ctx.ReadValue<float>() == 0f)
            {
                _characterFST.movementType = CharacterFST.MovementType.Walk;
            }


            sprintInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            _characterFST.movementType = CharacterFST.MovementType.Crouch;

            if (ctx.ReadValue<float>() == 0f)
            {
                _characterFST.movementType = CharacterFST.MovementType.Walk;
            }

            crouchInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            jumpInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        void OpenShopInputReceiver(InputAction.CallbackContext ctx)
        {
            //openShopInputValue = ctx.ReadValue<bool>();
        }

        void ShootInputReceiver(InputAction.CallbackContext ctx)
        {
            //shootInputValue = ctx.ReadValue<bool>();
        }

        void AimInputReceiver(InputAction.CallbackContext ctx)
        {
            //aimInputValue = ctx.ReadValue<bool>();
        }

        void DropItemInputReceiver(InputAction.CallbackContext ctx)
        {
            //dropItemInputValue = ctx.ReadValue<bool>();
        }
    
        void DropAmmoInputReceiver(InputAction.CallbackContext ctx)
        {
            //dropAmmoInputValue = ctx.ReadValue<bool>();
        }
    }
}
