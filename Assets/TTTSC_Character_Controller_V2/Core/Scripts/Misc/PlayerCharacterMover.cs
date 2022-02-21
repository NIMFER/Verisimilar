using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(PlayerInputReceiver))]
    public class PlayerCharacterMover : MonoBehaviour
    {
        private Vector3 _wallColliderNormalScale, _objectColliderNormalScale, _wallColliderCrouchedScale, _objectColliderCrouchedScale, _wallColliderNormalPosition, _wallColliderCrouchedPosition, _objectColliderNormalPosition, _objectColliderCrouchedPosition;
        [SerializeField]
        private float _crouchInterpolationStage = 1;

        private CharacterFST _characterFST;

        CharacterConfig _config;
        bool _walkPerforming;
        Vector2 _walkInput;
        private void OnEnable()
        {
            
            _characterFST = GetComponent<CharacterFST>();
            _config = GetComponent<CharacterConfig>();
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
            _wallColliderNormalScale = _config.wallCollider.localScale;
            _wallColliderCrouchedScale = new Vector3(_wallColliderNormalScale.x, _wallColliderNormalScale.y / _config.crouchHeight, _wallColliderNormalScale.z);
            _wallColliderNormalPosition = _config.wallCollider.localPosition;
            _wallColliderCrouchedPosition = new Vector3(_wallColliderNormalPosition.x, _wallColliderNormalPosition.y / _config.crouchHeight, _wallColliderNormalPosition.z);
            _objectColliderNormalScale = _config.objectCollider.localScale;
            _objectColliderCrouchedScale = new Vector3(_objectColliderNormalScale.x, _objectColliderNormalScale.y / _config.crouchHeight, _objectColliderNormalScale.z);
            _objectColliderNormalPosition = _config.objectCollider.localPosition;
            _objectColliderCrouchedPosition = new Vector3(_objectColliderNormalPosition.x, _objectColliderNormalPosition.y / _config.crouchHeight, _objectColliderNormalPosition.z);

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
                //Debug.Log("on ground");
            } 
            else if(GetComponent<CharacterFST>().characterState == CharacterFST.CharacterState.InAir && !GetComponent<CharacterFST>().topOnLadder)
            {
                MoveInAir();
                //Debug.Log("in air");
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
            Rigidbody rb = _config.characterRigidbody;
            Vector2 move = new Vector2(_walkInput.x * 225 * _config.walkSpeed * Time.deltaTime, _walkInput.y * 225 * _config.walkSpeed * Time.deltaTime);
        

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
                    move *= _config.sprintSpeedIncrease;
                    break;
                case CharacterFST.MovementType.Crouch:
                    move /= _config.crouchSpeedDecrease;
                    break;
            }

            //Debug.Log("current force = " + move);
            if (_walkPerforming)
            {
                rb.AddForce(transform.forward * move.y - rb.velocity + transform.right * move.x - rb.velocity, ForceMode.Impulse);
                //rb.velocity = transform.right * move.x + transform.up * rb.velocity.y + transform.forward * move.y;
                if (_characterFST.eligibleForStep && _characterFST.movementType != CharacterFST.MovementType.Crouch)
                    rb.position -= new Vector3(0f, -_config.stepHeight, 0f);
            }

        }

        private void MoveInAir()
        {
            Rigidbody rb = _config.characterRigidbody;
            Vector2 move = new Vector2(_walkInput.x * 225 * _config.airControlStrength * Time.deltaTime, _walkInput.y * 225 * _config.airControlStrength * Time.deltaTime);

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
            var wallColliderTransform = _config.wallCollider.transform;
            var environmentCollider = _config.environmentCollider;
            var objectCollider = _config.objectCollider;


            if (_crouchInterpolationStage > 0)
            {
                _crouchInterpolationStage -= _config.crouchSmoothing * Time.deltaTime;
                wallColliderTransform.localScale = Vector3.Lerp(_wallColliderCrouchedScale, _wallColliderNormalScale, _crouchInterpolationStage);
                wallColliderTransform.localPosition = Vector3.Lerp(_wallColliderCrouchedPosition, _wallColliderNormalPosition, _crouchInterpolationStage);
                environmentCollider.height = Mathf.Lerp(1f, 2f, _crouchInterpolationStage);
                objectCollider.localScale = Vector3.Lerp(_objectColliderCrouchedScale, _objectColliderNormalScale, _crouchInterpolationStage);
                objectCollider.localPosition = Vector3.Lerp(_objectColliderCrouchedPosition, _objectColliderNormalPosition, _crouchInterpolationStage);
            }


        }

        private void UnCrouch()
        {
            var wallColliderTransform = _config.wallCollider.transform;
            var environmentCollider = _config.environmentCollider;
            var objectCollider = _config.objectCollider;

            if (_crouchInterpolationStage < 1)
            {
                _crouchInterpolationStage += _config.crouchSmoothing * Time.deltaTime;
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
                    Rigidbody rb = _config.characterRigidbody;
                    rb.AddForce(transform.up * _config.jumpPower, ForceMode.Impulse);
                    break;
            }

        }

    }
}
