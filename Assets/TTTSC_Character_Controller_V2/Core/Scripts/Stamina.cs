using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TTTSC_Character_Controller_V2.Core.Scripts;
using System.Threading.Tasks;
using System;

public class Stamina : MonoBehaviour
{
    CharacterConfig _characterConfig;
    CharacterFST _characterFST;
    PlayerInputReceiver _playerInputReceiver;



    // Start is called before the first frame update
    void Start()
    {
        _characterConfig = GetComponent<CharacterConfig>();
        _characterFST = GetComponent<CharacterFST>();
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();

        _playerInputReceiver.sprintInputEvent += Sprint;
    }

    private void Sprint(float performing)
    {
        switch (performing)
        {
            case 0:
                StopCoroutine("TakeStamina");
                StartCoroutine("RegenStamina");
                break;
            case 1:
                StartCoroutine("TakeStamina");
                StopCoroutine("RegenStamina");
                break;
        }


    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator RegenStamina()
    {
        Debug.Log("regenerating stamina");

        if (_characterFST.outOfBreath)
        {

            Debug.Log("you are out of breath");
            new WaitForSecondsRealtime(20);
            Debug.Log("wait time is over");

            Debug.Log("player is not running");

            while (_characterFST.movementType != CharacterFST.MovementType.Sprint)
            {

                if (_characterConfig.currentStamina >= _characterConfig.maxStamina)
                {
                    Debug.Log("player has full stamina");
                    _characterFST.outOfBreath = false;
                    break;
                }
                else
                {
                    Debug.Log("stamina is not full yet");
                    _characterConfig.currentStamina += _characterConfig.staminaDepleation;
                    yield return new WaitForSeconds(_characterConfig.staminaDepleationSpeed * 12);
                }
            }


        }
        else
        {
            Debug.Log("player is not running");

            while (_characterFST.movementType != CharacterFST.MovementType.Sprint)
            {

                if (_characterConfig.currentStamina >= _characterConfig.maxStamina)
                {
                    Debug.Log("player has full stamina");
                    break;
                }
                else
                {
                    Debug.Log("stamina is not full yet");
                    _characterConfig.currentStamina += _characterConfig.staminaDepleation;
                    yield return new WaitForSeconds(_characterConfig.staminaDepleationSpeed * 10);
                }
            }
        }



        new WaitForSecondsRealtime(1);
    }

    public IEnumerator TakeStamina()
    {
        if (_characterFST.movementType == CharacterFST.MovementType.Sprint && _characterConfig.currentStamina >= 1)
        {
            Debug.Log("player is running");

            while (_characterFST.movementType == CharacterFST.MovementType.Sprint)
            {

                if (_characterConfig.currentStamina == 0)
                {
                    Debug.Log("No stamina left");
                    _characterFST.outOfBreath = true;
                    yield return new WaitForSeconds(1);
                    break;
                }
                else
                {
                    Debug.Log("player still has stamina");
                    yield return new WaitForSeconds(_characterConfig.staminaDepleationSpeed);
                    _characterConfig.currentStamina -= _characterConfig.staminaDepleation;
                }
            }
        }

        new WaitForSecondsRealtime(1);
    }
}
