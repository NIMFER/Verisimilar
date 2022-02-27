using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVideoSetting : MonoBehaviour
{

    public Dropdown FullscreenModeDropDown;
    public Dropdown ResolutionDropDown;
    public Dropdown QualityDropDown;

    public List<string> FullscreenModeString;
    public List<string> QualityString;

    // Start is called before the first frame update
    void Start()
    {
        FullscreenModeDropDown.ClearOptions();
        ResolutionDropDown.ClearOptions();
        QualityDropDown.ClearOptions();

        Resolution[] resolutions = Screen.resolutions;
        Resolution currentResolution = Screen.currentResolution;

        foreach (string text in FullscreenModeString)
        {
            FullscreenModeDropDown.options.Add(new Dropdown.OptionData() { text = text });
        }
        foreach (Resolution text in resolutions)
        {
            ResolutionDropDown.options.Add(new Dropdown.OptionData() { text = text.width + "x" + text.height });
        }
        foreach (string text in QualityString)
        {
            QualityDropDown.options.Add(new Dropdown.OptionData() { text = text });
        }

        if (Screen.fullScreen)
        {
            FullscreenModeDropDown.SetValueWithoutNotify(0);
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (currentResolution.width == resolutions[i].width && currentResolution.height == resolutions[i].height)
                {
                    ResolutionDropDown.SetValueWithoutNotify(i);
                    break;
                }
            }
        }
        else
        {
            FullscreenModeDropDown.SetValueWithoutNotify(1);
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (Screen.width == resolutions[i].width && Screen.height == resolutions[i].height)
                {
                    ResolutionDropDown.SetValueWithoutNotify(i);
                    break;
                }
            }
        }
        QualityDropDown.SetValueWithoutNotify(QualitySettings.GetQualityLevel());

        FullscreenModeDropDown.RefreshShownValue();
        ResolutionDropDown.RefreshShownValue();
        QualityDropDown.RefreshShownValue();
    }

    public void setSetting()
    {

        string quality = QualityDropDown.options[QualityDropDown.value].text;

        for (int i = 0; i < QualityString.Count; i++)
        {
            if (quality == QualityString[i])
            {
                QualitySettings.SetQualityLevel(i, true);
                break;
            }
        }

        string fullscreen = FullscreenModeDropDown.options[FullscreenModeDropDown.value].text;
        string resolution = ResolutionDropDown.options[ResolutionDropDown.value].text;
        Resolution[] resolutions = Screen.resolutions;

        if (fullscreen == FullscreenModeString[0])
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolution == resolutions[i].width + "x" + resolutions[i].height)
                {
                    Screen.SetResolution(resolutions[i].width, resolutions[i].height, true);
                    break;
                }
            }
        }
        else if (fullscreen == FullscreenModeString[1])
        {
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolution == resolutions[i].width + "x" + resolutions[i].height)
                {
                    Screen.SetResolution(resolutions[i].width, resolutions[i].height, false);
                    break;
                }
            }
        }

    }

}
