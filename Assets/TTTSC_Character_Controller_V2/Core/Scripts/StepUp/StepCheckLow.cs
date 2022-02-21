using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.StepUp
{
    public class StepCheckLow : MonoBehaviour
    {
        public bool stepCheckLowTriggered;
        [SerializeField]
        private LayerMask _layers;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != _layers)
                stepCheckLowTriggered = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer != _layers)
                stepCheckLowTriggered = false;
        }
    }
}
