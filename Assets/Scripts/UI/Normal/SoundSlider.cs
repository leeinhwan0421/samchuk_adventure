using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SoundSlider : MonoBehaviour
{
    private Slider slider;

    [SerializeField] private Text text;

    [Header("Option")]
    public bool isSFX;
    public bool isBGM;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }

    private void OnEnable()
    {
        if (isSFX)
        {
            slider.value = SoundInstance.sfxValue;
        }
        else if (isBGM)
        {
            slider.value = SoundInstance.musicValue;
        }

        float v = (slider.value * 100f);

        text.text = ((int)v).ToString();
    }

    public void ChangeValue()
    {
        if (isSFX)
        {
            SoundInstance.sfxValue = slider.value;
        }
        else if (isBGM)
        {
            SoundInstance.Instance.ChangeBGMSoundValue(slider.value);
        }

        float v = (slider.value * 100f);

        text.text = (((int)v).ToString());
    }
}
