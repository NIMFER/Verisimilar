using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.StepUp
{
    public class StepCheckHigh : MonoBehaviour
    {
        public bool stepCheckHighTriggered;
        [SerializeField]
        private LayerMask _layers;

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer != _layers)
                stepCheckHighTriggered = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer != _layers)
                stepCheckHighTriggered = false;
        }
    }
}
