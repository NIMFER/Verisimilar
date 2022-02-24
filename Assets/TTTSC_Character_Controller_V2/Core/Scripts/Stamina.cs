using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTTSC_Character_Controller_V2.Core.Scripts;
using System.Threading.Tasks;
using System;

public class Stamina : MonoBehaviour
{
    CharacterConfig _characterConfig;
    CharacterFST _characterFST;
    PlayerInputReceiver _playerInputReceiver;
    [SerializeField]
    Slider _staminaSlider;


    // Start is called before the first frame update
    void Start()
    {
        _characterConfig = GetComponent<CharacterConfig>();
        _characterFST = GetComponent<CharacterFST>();
        _playerInputReceiver = GetComponent<PlayerInputReceiver>();

        _playerInputReceiver.SprintInputEvent += Sprint;
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
    void FixedUpdate()
    {
        _staminaSlider.maxValue = _characterConfig.maxStamina;
        _staminaSlider.value = _characterConfig.currentStamina;
    }

    public IEnumerator RegenStamina()
    {
        Debug.Log("regenerating stamina");

        if (_characterFST.outOfBreath)
        {

            Debug.Log("you are out of breath");
            new WaitForSecondsRealtime(20f);
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
                    yield return new WaitForSecondsRealtime(_characterConfig.staminaDepleationSpeed * 12);
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
                    yield return new WaitForSecondsRealtime(_characterConfig.staminaDepleationSpeed * 10);
                }
            }
        }



        new WaitForSecondsRealtime(1);
    }

    public IEnumerator TakeStamina()
    {

        while (true)
        {
            if (_characterFST.movementType == CharacterFST.MovementType.Sprint && _characterConfig.currentStamina > 0)
            {

                while (_characterFST.movementState == CharacterFST.MovementState.Moving)
                {
                    Debug.Log("player is running");

                    if (_characterConfig.currentStamina == 0)
                    {
                        Debug.Log("No stamina left");
                        _characterFST.outOfBreath = true;
                        yield return new WaitForSecondsRealtime(1);
                        break;
                    }
                    else
                    {
                        Debug.Log("player still has stamina");
                        yield return new WaitForSecondsRealtime(_characterConfig.staminaDepleationSpeed);
                        _characterConfig.currentStamina -= _characterConfig.staminaDepleation;
                    }
                }
            }


            yield return new WaitForSecondsRealtime(0.1f);
        }


    }
}
