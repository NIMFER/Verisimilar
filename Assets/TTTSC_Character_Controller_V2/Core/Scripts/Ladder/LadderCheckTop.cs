using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Ladder
{
    public class LadderCheckTop : MonoBehaviour
    {

        public Transform detectedLadder;
    
        public bool ladderCheckTopTriggered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ladder"))
            {
                ladderCheckTopTriggered = true;
                detectedLadder = other.gameObject.transform;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ladder"))
            {
                ladderCheckTopTriggered = false;
                detectedLadder = null;
            }
        }
    }
}
