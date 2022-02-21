using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Ladder
{
    public class LadderClimbing : MonoBehaviour
    {
        private CharacterConfig _config;
        private Vector2 _walkInput;
        private Rigidbody _rb;
        private bool _onLadder;
        private bool _topOnLadder;
        private bool _bottomOnLadder;
        private bool _enteredLadderFromBottom;
        private bool _enteredLadderFromTop;
        private bool _walkPerforming;
        
        private void OnEnable()
        {
            _config = GetComponent<CharacterConfig>();
            _rb = GetComponent<Rigidbody>();
            GetComponent<PlayerInputReceiver>().walkInputEvent += WalkInput;
        }

        private void OnDisable()
        {
            GetComponent<PlayerInputReceiver>().walkInputEvent -= WalkInput;
        }

        private void WalkInput(Vector2 walkInputValue, bool performing)
        {
            _walkInput = walkInputValue;
            _walkPerforming = performing;
        }

        // Update is called once per frame
        private void Update()
        {
            _onLadder = GetComponent<CharacterFST>().onLadder;
            _topOnLadder = GetComponent<CharacterFST>().topOnLadder;
            _bottomOnLadder = GetComponent<CharacterFST>().bottomOnLadder;
            _enteredLadderFromBottom = GetComponent<CharacterFST>().enteredLadderFromBottom;
            _enteredLadderFromTop = GetComponent<CharacterFST>().enteredLadderFromTop;

            if(_topOnLadder && !_bottomOnLadder && !_enteredLadderFromBottom && !_enteredLadderFromTop) 
            {
                GetComponent<CharacterFST>().enteredLadderFromBottom = true;
            }


            
            if(!_topOnLadder && _bottomOnLadder && !_enteredLadderFromTop && !_enteredLadderFromBottom)
            {
                GetComponent<CharacterFST>().enteredLadderFromBottom = true;
            }

            switch (_onLadder)
            {
                case false:
                    GetComponent<CharacterFST>().enteredLadderFromTop = false;
                    GetComponent<CharacterFST>().enteredLadderFromBottom = false;
                    if (!_rb.useGravity)
                    {
                        _rb.useGravity = true;
                    }
                    break;
                case true:
                    WalkingUpLadder();
                    if (_rb.useGravity)
                    {
                        _rb.useGravity = false;
                    }
                    break;
            }
        

        }

        private void WalkingUpLadder()
        {
            //Collider ladder = GetComponent<CharacterFST>().topLadder;

            //Vector3 rbPosition = rb.position;
            //Vector3 ladderPosition = ladder.transform.position;
            Vector2 moveWS = new Vector2(_walkInput.x / 200 * _config.ladderClimbingSpeed * Time.deltaTime, _walkInput.y / 200 * _config.ladderClimbingSpeed * Time.deltaTime);
            Vector2 moveAD = new Vector2(_walkInput.x / 200 * _config.walkSpeed * Time.deltaTime, _walkInput.y / 200 * _config.walkSpeed * Time.deltaTime);
            
            Debug.Log(_walkInput.y);
            
            if (_enteredLadderFromBottom && !_topOnLadder && _walkInput.y >= 0)
            {

            }
            else if (_enteredLadderFromTop && !_topOnLadder )
            {
                    
            }
            
        }
        
    }
}
