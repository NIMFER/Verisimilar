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
    
        public event Action<Vector2, bool> WalkInputEvent, LookInputEvent;
        public event Action<float> SprintInputEvent, CrouchInputEvent, JumpInputEvent, InventoryInputEvent, InteractInputEvent;


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
            playerInputEvents.Controlls.Inventory.performed += OpenInventoryInputReceiver;
            playerInputEvents.Controlls.Interact.performed += InteractInputReceiver;
        }

        private void OnDisable()
        {
            playerInputEvents.Disable();
            playerInputEvents.Controlls.Walk.performed -= WalkInputReceiver;
            playerInputEvents.Controlls.LookX.performed -= LookXInputReceiver;
            playerInputEvents.Controlls.Sprint.performed -= SprintInputReceiver;
            playerInputEvents.Controlls.Jump.performed -= JumpInputReceiver;
            playerInputEvents.Controlls.Crouch.performed -= CrouchInputReceiver;
            playerInputEvents.Controlls.Inventory.performed -= OpenInventoryInputReceiver;
            playerInputEvents.Controlls.Interact.performed -= InteractInputReceiver;

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

            LookInputEvent?.Invoke(look, performing);
        }

        private void WalkInputReceiver(InputAction.CallbackContext ctx)
        {
            var value = ctx.ReadValue<Vector2>();

            bool performing = !(value == new Vector2(0,0));

            WalkInputEvent?.Invoke(value, performing);
        }

        private void SprintInputReceiver(InputAction.CallbackContext ctx)
        {
            if(_characterFST.movementType != CharacterFST.MovementType.Crouch & GetComponent<CharacterConfig>().allowSprint == true)
                _characterFST.movementType = CharacterFST.MovementType.Sprint;

            if (ctx.ReadValue<float>() == 0f)
            {
                _characterFST.movementType = CharacterFST.MovementType.Walk;
            }


            SprintInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        private void CrouchInputReceiver(InputAction.CallbackContext ctx)
        {
            _characterFST.movementType = CharacterFST.MovementType.Crouch;

            if (ctx.ReadValue<float>() == 0f)
            {
                _characterFST.movementType = CharacterFST.MovementType.Walk;
            }

            CrouchInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        private void JumpInputReceiver(InputAction.CallbackContext ctx)
        {
            JumpInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        void OpenInventoryInputReceiver(InputAction.CallbackContext ctx)
        {
            InventoryInputEvent?.Invoke(ctx.ReadValue<float>());
        }

        void InteractInputReceiver(InputAction.CallbackContext ctx)
        {
            InteractInputEvent?.Invoke(ctx.ReadValue<float>());
        }
    }
}
