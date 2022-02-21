using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.StepUp
{
    public class StepCheck : MonoBehaviour
    {
        void FixedUpdate()
        {
            if (GetComponentInChildren<StepCheckLow>().stepCheckLowTriggered && !GetComponentInChildren<StepCheckHigh>().stepCheckHighTriggered)
            {
                GetComponent<CharacterFST>().eligibleForStep = true;
            }
            else
            {
                GetComponent<CharacterFST>().eligibleForStep = false;
            }
        }
    }
}
