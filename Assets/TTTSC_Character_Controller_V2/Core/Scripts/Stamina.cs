using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TTTSC_Character_Controller_V2.Core.Scripts;
using System.Threading.Tasks;
using System;

public class Stamina : MonoBehaviour
{
    [SerializeField]
    CanvasGroup canvasGroup;
    CharacterConfig _characterConfig;
    CharacterFST _characterFST;
    PlayerInputReceiver _playerInputReceiver;
    [SerializeField]
    Slider _staminaSlider;
    bool _regeneratingStamina;


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
        if (performing == 0 && !_characterFST.outOfBreath)
        {
            StopCoroutine("TakeStamina");
            StartCoroutine("RegenStamina");
        }
        else if(performing == 0 && _characterFST.outOfBreath)
        {
            _characterConfig.allowSprint = false;
            StopCoroutine("TakeStamina");
            StartCoroutine("RegenStaminaOOB");
        }
        else if (performing == 1 && !_characterFST.outOfBreath)
        {

            StartCoroutine("TakeStamina");
            StopCoroutine("RegenStamina");
            StopCoroutine("HideStaminaBarGradualy");
            canvasGroup.alpha = 1;
        }


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _staminaSlider.maxValue = _characterConfig.maxStamina;
        _staminaSlider.value = _characterConfig.currentStamina;
    }

    public IEnumerator RegenStaminaOOB()
    {
        Debug.Log("regenerating stamina");

        Debug.Log("you are out of breath");
        new WaitForSecondsRealtime(10);
        Debug.Log("wait time is over");

        Debug.Log("player is not running");
        if (!_regeneratingStamina)
        {
            _regeneratingStamina = true;
            while (_characterFST.movementType != CharacterFST.MovementType.Sprint)
            {

                if (_characterConfig.currentStamina >= _characterConfig.maxStamina)
                {
                    Debug.Log("player has full stamina");
                    StartCoroutine("HideStaminaBarGradualy");
                    _regeneratingStamina = false;   
                    _characterFST.outOfBreath = false;
                    _characterConfig.allowSprint = true;
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

        new WaitForSecondsRealtime(1);
    }

    public IEnumerator RegenStamina()
    {
        Debug.Log("regenerating stamina");
        Debug.Log("player is not running");

        while (_characterFST.movementType != CharacterFST.MovementType.Sprint)
        {

            if (_characterConfig.currentStamina >= _characterConfig.maxStamina)
            {
                Debug.Log("player has full stamina");
                StartCoroutine("HideStaminaBarGradualy");
                break;
            }
            else
            {
                Debug.Log("stamina is not full yet");
                _characterConfig.currentStamina += _characterConfig.staminaDepleation;
                yield return new WaitForSecondsRealtime(_characterConfig.staminaDepleationSpeed * 10);
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

    public IEnumerator HideStaminaBarGradualy()
    {
        Debug.Log("hiding stamina bar");
        while (true)
        {
            if (_characterFST.movementType != CharacterFST.MovementType.Sprint)
            {
                
                yield return new WaitForSecondsRealtime(10);
                Debug.Log("hiding bar now");


                while (_characterFST.movementType != CharacterFST.MovementType.Sprint)
                {

                    if (canvasGroup.alpha == 0)
                    {
                        yield return new WaitForSecondsRealtime(1);
                        Debug.Log("bar is hidden");
                        break;
                    }
                    else
                    {
                        yield return new WaitForSecondsRealtime(0.1f);
                        canvasGroup.alpha -= 0.1f;
                        Debug.Log("lovering alpha");
                    }
                }
            }


            yield return new WaitForSecondsRealtime(0.1f);
        }


    }
}
