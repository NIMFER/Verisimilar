using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.StepUp
{
    public class StepCheckHigh : MonoBehaviour
    {
        public bool stepCheckHighTriggered;
        [SerializeField]
        private LayerMask _stepUpLayer;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == _stepUpLayer)
            {
                stepCheckHighTriggered = true;

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer != _stepUpLayer)
            {
                stepCheckHighTriggered = false;
            }
        }
    }
}
