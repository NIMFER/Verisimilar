using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    public class GroundCheck : MonoBehaviour
    {
        [SerializeField]
        private Color _gizmoColor;
        [SerializeField]
        private Vector3 _groundCheckPosition;
        [SerializeField]
        private float _groundCheckRadius;
        [SerializeField]
        LayerMask _enviormentLayer;

        private Collider[] enviormentDetected;

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            if (GetComponent<CharacterFST>().movementType == CharacterFST.MovementType.Crouch)
            {
                Gizmos.DrawSphere(gameObject.transform.position + _groundCheckPosition/2f, _groundCheckRadius);
            }
            else
            {
                Gizmos.DrawSphere(gameObject.transform.position + _groundCheckPosition, _groundCheckRadius);
            }
        }

        void FixedUpdate()
        {
        
            if(GetComponent<CharacterFST>().movementType == CharacterFST.MovementType.Crouch)
            {
                enviormentDetected = Physics.OverlapSphere(gameObject.transform.position + _groundCheckPosition/2f, _groundCheckRadius, _enviormentLayer, QueryTriggerInteraction.Ignore);
            }
            else
            {
                enviormentDetected = Physics.OverlapSphere(gameObject.transform.position + _groundCheckPosition, _groundCheckRadius, _enviormentLayer, QueryTriggerInteraction.Ignore);
            }

            if (enviormentDetected.Length <= 0)
            {
                GetComponent<CharacterFST>().characterState = CharacterFST.CharacterState.InAir;
            }
            else
            {
                GetComponent<CharacterFST>().characterState = CharacterFST.CharacterState.OnGround;
            }
        }
    }
}
