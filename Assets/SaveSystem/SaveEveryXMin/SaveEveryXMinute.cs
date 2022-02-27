using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveEveryXMinute : MonoBehaviour
{

    public SaveData SD; // SaveData
    public float setMinute;
    float currentMinute;

    // Start is called before the first frame update
    void Start()
    {
        setMinute *= 60;
        currentMinute = setMinute;
    }

    // Update is called once per frame
    void Update()
    {
        currentMinute -= Time.deltaTime;
        if (currentMinute <= 0)
        {
            // This script is an example of saving
            //GameObject.Find("SaveSystem").GetComponent<SaveData>().save(SD.index);
            currentMinute = setMinute;
        }
    }
}
