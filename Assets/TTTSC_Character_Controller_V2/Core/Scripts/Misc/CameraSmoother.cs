using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    public class CameraSmoother : MonoBehaviour
    {
        [SerializeField]
        private Transform _camera, _cameraTarget;
        [SerializeField]
        private float _camSmoothingSpeed;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            CharacterConfig config = GetComponent<CharacterConfig>();
            CharacterFST characterFST = GetComponent<CharacterFST>();

            Vector3 currentPosition = new Vector3(_camera.position.x, _camera.position.y, _camera.position.z);
            Vector3 desieredPosition = new Vector3(_cameraTarget.position.x, _cameraTarget.position.y, _cameraTarget.position.z);
            float smoothedYPosition = Mathf.Lerp(currentPosition.y,desieredPosition.y, _camSmoothingSpeed);
            _camera.rotation = _cameraTarget.rotation;

            float cameraYDistance = Mathf.Abs(desieredPosition.y - currentPosition.y);

            if (currentPosition != desieredPosition)
            {
                _camera.position = new Vector3(desieredPosition.x, smoothedYPosition, desieredPosition.z);
            }

        }
    }
}
