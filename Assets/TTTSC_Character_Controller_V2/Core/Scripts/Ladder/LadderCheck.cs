using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts.Ladder
{
    public class LadderCheck : MonoBehaviour
    {
        private LadderCheckTop _ladderCheckTop;
        private LadderCheckBottom _ladderCheckBottom;

        // Update is called once per frame
        void FixedUpdate()
        {
            _ladderCheckTop = GetComponentInChildren<LadderCheckTop>();
            _ladderCheckBottom = GetComponentInChildren<LadderCheckBottom>();
            
            switch (_ladderCheckTop.ladderCheckTopTriggered)
            {
                case true:
                    GetComponent<CharacterFST>().topOnLadder = true;
                    GetComponent<CharacterFST>().topLadder = _ladderCheckTop.detectedLadder;
                    break;
                case false:
                    GetComponent<CharacterFST>().topOnLadder = false;
                    GetComponent<CharacterFST>().topLadder = null;
                    break;;
            }
            
            switch (GetComponentInChildren<LadderCheckBottom>().ladderCheckBottomTriggered)
            {
                case true:
                GetComponent<CharacterFST>().bottomOnLadder = true;
                GetComponent<CharacterFST>().bottomLadder = _ladderCheckBottom.detectedLadder;
                    break;
                case false:
                    GetComponent<CharacterFST>().bottomOnLadder = false;
                    GetComponent<CharacterFST>().bottomLadder = null;
                    break;;
            }

            if (GetComponent<CharacterFST>().bottomOnLadder || GetComponent<CharacterFST>().topOnLadder)
            {
                GetComponent<CharacterFST>().onLadder = true;
            }
            else
            {
                GetComponent<CharacterFST>().onLadder = false;
            }
        }
    }
}
