using UnityEngine;

namespace TTTSC_Character_Controller_V2.Core.Scripts
{
    public class CharacterConfig : MonoBehaviour
    {
        public Rigidbody characterRigidbody; // Rigidbody that will be used for moving character around
        public CapsuleCollider environmentCollider; // Collider for enviorment (map and other static objects)
        public Transform wallCollider; // Collider for walls (it has no friction and fixes sticking to walls)
        public BoxCollider[] characterHitboxes; // Hidboxes for character model
        public float walkSpeed; // This value controlls at what speed your character walks
        public float airControlStrength; // This value controlls strenght of the force applied to the player when in the air
        public float crouchSpeedDecrease; // This value controlls how much slower are you while crouching (equasion: walkSpeed / crouchSpeedDecrease) 
        public float sprintSpeedIncrease; // This value controlls how much faster are you while sprinting (equasion: walkSpeed * sprintSpeedIncrease)
        public float ladderClimbingSpeed; // This value controlls how fast dose the character climb ladders
        public float jumpPower; // This value controlls character's jump height
        public float crouchHeightDecrease; // This value controlls the hight of player when crouched
        public float crouchSmoothing; // This value controlls the transition speed of standing to crouch and viceversa
        public float lookVerticalSpeed; // This value controlls vertical looking speed
        public float lookHorizontalSpeed; // This value controlls horizontal looking speed
        public float aimVerticalSpeed; // This value controlls vertical looking speed while aiming down the sight
        public float aimHorizontalSpeed; // This value controlls horizontal looking speed while aiming down the sight
        public float stepHeight; // This value controlls how high steps charecter takes
        public bool allowSprint; // bool for enabling sprint
        public bool allowJump; // bool for enabling jumping
    }
}