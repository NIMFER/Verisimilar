using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Misc
{
    public class CeilingCheck : MonoBehaviour
    {
        [SerializeField]
        private Color _gizmoColor;
        [SerializeField]
        private Vector3 _ceilingCheckPosition, _ceilingCheckSize;
        [SerializeField]
        private LayerMask layers;

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(gameObject.transform.position + _ceilingCheckPosition, _ceilingCheckSize);
        }

        void FixedUpdate()
        {
            Quaternion quaternion = Quaternion.identity;
            Collider[] enviormentDetected = Physics.OverlapBox(gameObject.transform.position + _ceilingCheckPosition, _ceilingCheckSize, quaternion, layers);

            //Debug.Log(gameObject.transform.position + _ceilingCheckPosition);

            if (enviormentDetected.Length <= 0)
            {
                GetComponent<CharacterFST>().ceilingDetected = false;
            }
            else
            {
                GetComponent<CharacterFST>().ceilingDetected = true;
            }
        }
    }
}
