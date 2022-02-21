using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputReceiver))]
    public class PlayerCharacterMover : MonoBehaviour
    {
        private Vector3 _wallColliderNormalScale, _objectColliderNormalScale, _wallColliderCrouchedScale, _objectColliderCrouchedScale, _wallColliderNormalPosition, _wallColliderCrouchedPosition, _objectColliderNormalPosition, _objectColliderCrouchedPosition;
        private float _crouchInterpolationStage = 1;
        private CharacterFST _characterFST;
        private CharacterConfig _characterConfig;
        bool _walkPerforming;
        private Vector2 _walkInput;

        [SerializeField]
        private Transform _camTransform;

        private void OnEnable()
        {
            _characterFST = GetComponent<CharacterFST>();
            _characterConfig = GetComponent<CharacterConfig>();
            GetComponent<PlayerInputReceiver>().jumpInputEvent += Jump;
            GetComponent<PlayerInputReceiver>().walkInputEvent += WalkInput;
        }

        private void OnDisable()
        {
            GetComponent<PlayerInputReceiver>().jumpInputEvent -= Jump;
            GetComponent<PlayerInputReceiver>().walkInputEvent -= WalkInput;
        }

        void Start()
        {
            _wallColliderNormalScale = _characterConfig.wallCollider.localScale;
            _wallColliderCrouchedScale = new Vector3(_wallColliderNormalScale.x, _wallColliderNormalScale.y / _characterConfig.crouchHeight, _wallColliderNormalScale.z);
            _wallColliderNormalPosition = _characterConfig.wallCollider.localPosition;
            _wallColliderCrouchedPosition = new Vector3(_wallColliderNormalPosition.x, _wallColliderNormalPosition.y / _characterConfig.crouchHeight, _wallColliderNormalPosition.z);
            _objectColliderNormalScale = _characterConfig.objectCollider.localScale;
            _objectColliderCrouchedScale = new Vector3(_objectColliderNormalScale.x, _objectColliderNormalScale.y / _characterConfig.crouchHeight, _objectColliderNormalScale.z);
            _objectColliderNormalPosition = _characterConfig.objectCollider.localPosition;
            _objectColliderCrouchedPosition = new Vector3(_objectColliderNormalPosition.x, _objectColliderNormalPosition.y / _characterConfig.crouchHeight, _objectColliderNormalPosition.z);

        }

        private void WalkInput(Vector2 walkInputValue, bool performing)
        {
            _walkPerforming = performing;
            _walkInput = walkInputValue;
        }

        private void FixedUpdate()
        {
            if (GetComponent<CharacterFST>().characterState == CharacterFST.CharacterState.OnGround && !GetComponent<CharacterFST>().topOnLadder)
            {
                Move();
            } 
            else if(GetComponent<CharacterFST>().characterState == CharacterFST.CharacterState.InAir && !GetComponent<CharacterFST>().topOnLadder)
            {
                MoveInAir();
            }


            switch (GetComponent<CharacterFST>().movementType)
            {
                default:
                    if(!GetComponent<CharacterFST>().ceilingDetected)
                        UnCrouch();
                    break;
                case CharacterFST.MovementType.Crouch:
                    Crouch();
                    break;
            }
        }

        private void Move()
        {
            Rigidbody rb = _characterConfig.characterRigidbody;
            Vector2 move = new Vector2(_walkInput.x * 225 * _characterConfig.walkSpeed * Time.deltaTime, _walkInput.y * 225 * _characterConfig.walkSpeed * Time.deltaTime);
        

            if (_walkInput == new Vector2(1, 0) || _walkInput == new Vector2(-1, 0) || _walkInput == new Vector2(0, 1) || _walkInput == new Vector2(0, -1))
            {

            }
            else
            {
                move /= 1.4f;
            }


            switch (_characterFST.movementType)
            {
                case CharacterFST.MovementType.Sprint:
                    move *= _characterConfig.sprintSpeedIncrease;
                    break;
                case CharacterFST.MovementType.Crouch:
                    move /= _characterConfig.crouchSpeedDecrease;
                    break;
            }

            if (_walkPerforming)
            {
                rb.AddForce(transform.forward * move.y - rb.velocity + transform.right * move.x - rb.velocity, ForceMode.Impulse);
                if (_characterFST.eligibleForStep && _characterFST.movementType != CharacterFST.MovementType.Crouch)
                    rb.position -= new Vector3(0f, -_characterConfig.stepHeight, 0f);
            }

        }

        private void MoveInAir()
        {
            Rigidbody rb = _characterConfig.characterRigidbody;
            Vector2 move = new Vector2(_walkInput.x * 225 * _characterConfig.airControlStrength * Time.deltaTime, _walkInput.y * 225 * _characterConfig.airControlStrength * Time.deltaTime);

            if (_walkInput == new Vector2(1, 0) || _walkInput == new Vector2(-1, 0) || _walkInput == new Vector2(0, 1) || _walkInput == new Vector2(0, -1))
            {

            }
            else
            {
                move /= 1.4f;
            }

            if (_walkPerforming)
            {
                rb.AddForce(transform.right * move.x + transform.forward * move.y, ForceMode.Impulse);
            }
        }

        private void Crouch()
        {
            var wallColliderTransform = _characterConfig.wallCollider.transform;
            var environmentCollider = _characterConfig.environmentCollider;
            var objectCollider = _characterConfig.objectCollider;


            if (_crouchInterpolationStage > 0)
            {
                _crouchInterpolationStage -= _characterConfig.crouchSmoothing * Time.deltaTime;
                wallColliderTransform.localScale = Vector3.Lerp(_wallColliderCrouchedScale, _wallColliderNormalScale, _crouchInterpolationStage);
                wallColliderTransform.localPosition = Vector3.Lerp(_wallColliderCrouchedPosition, _wallColliderNormalPosition, _crouchInterpolationStage);
                environmentCollider.height = Mathf.Lerp(1f, 2f, _crouchInterpolationStage);
                objectCollider.localScale = Vector3.Lerp(_objectColliderCrouchedScale, _objectColliderNormalScale, _crouchInterpolationStage);
                objectCollider.localPosition = Vector3.Lerp(_objectColliderCrouchedPosition, _objectColliderNormalPosition, _crouchInterpolationStage);
            }


        }

        private void UnCrouch()
        {
            var wallColliderTransform = _characterConfig.wallCollider.transform;
            var environmentCollider = _characterConfig.environmentCollider;
            var objectCollider = _characterConfig.objectCollider;

            if (_crouchInterpolationStage < 1)
            {
                _crouchInterpolationStage += _characterConfig.crouchSmoothing * Time.deltaTime;
                wallColliderTransform.localScale = Vector3.Lerp(_wallColliderCrouchedScale, _wallColliderNormalScale, _crouchInterpolationStage); // For some reason this line crashes the editor
                wallColliderTransform.localPosition = Vector3.Lerp(_wallColliderCrouchedPosition, _wallColliderNormalPosition, _crouchInterpolationStage);
                environmentCollider.height = Mathf.Lerp(1f, 2f, _crouchInterpolationStage);
                objectCollider.localScale = Vector3.Lerp(_objectColliderCrouchedScale, _objectColliderNormalScale, _crouchInterpolationStage);
                objectCollider.localPosition = Vector3.Lerp(_objectColliderCrouchedPosition, _objectColliderNormalPosition, _crouchInterpolationStage);
            }


        }

        private void Jump(float jump)
        {

            switch (_characterFST.characterState)
            {
                case CharacterFST.CharacterState.OnGround:
                    Rigidbody rb = _characterConfig.characterRigidbody;
                    rb.AddForce(transform.up * _characterConfig.jumpPower, ForceMode.Impulse);
                    break;
            }

        }

    }
}
