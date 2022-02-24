using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.StepUp
{
    public class StepCheckLow : MonoBehaviour
    {
        public bool stepCheckLowTriggered;
        [SerializeField]
        private LayerMask _stepUpLayer;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != _stepUpLayer)
            {
                stepCheckLowTriggered = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer != _stepUpLayer)
            {
                stepCheckLowTriggered = false;
            }
        }
    }
}
