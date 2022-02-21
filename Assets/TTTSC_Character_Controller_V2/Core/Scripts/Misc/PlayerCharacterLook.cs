using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    public class PlayerCharacterLook : MonoBehaviour
    {
        private PlayerInputReceiver _playerInputReceiver;
        [SerializeField]
        private CharacterConfig _config;

        private bool _lookPerforming;

        private float _lookVertical, _lookHorizontal, _camRotation, _playerRotation;
        [SerializeField]
        private Transform _playerCharacterTransform, _camHolder;

        private void OnEnable()
        {
            GetComponent<PlayerInputReceiver>().lookInputEvent += LookInput;
        }

        private void OnDisable()
        {
            GetComponent<PlayerInputReceiver>().lookInputEvent -= LookInput;
        }

        private void LookInput(Vector2 lookInput, bool performing)
        {
            _lookPerforming = performing;

            _lookHorizontal = lookInput.x * _config.lookHorizontalSpeed * Time.deltaTime;
            _lookVertical = lookInput.y * _config.lookVerticalSpeed * Time.deltaTime;
        
        }

        private float _rotationX;
    
        // Update is called once per frame
        void Update()
        {
            _rotationX -= _lookVertical;
            _rotationX = Mathf.Clamp(_rotationX, -90, 90);
            _camHolder.localRotation = Quaternion.Euler(_rotationX,0,0);


            _playerCharacterTransform.Rotate(Vector3.up, _lookHorizontal);
        }
    }
}
