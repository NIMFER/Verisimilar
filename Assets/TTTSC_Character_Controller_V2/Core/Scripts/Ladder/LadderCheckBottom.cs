using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Ladder
{
    public class LadderCheckBottom : MonoBehaviour
    {

        public Transform detectedLadder;
        
        public bool ladderCheckBottomTriggered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ladder"))
            {
                ladderCheckBottomTriggered = true;
                detectedLadder = other.gameObject.transform;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Ladder"))
            {
                ladderCheckBottomTriggered = false;
                detectedLadder = null;
            }
        }
    }
}
