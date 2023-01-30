using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Fill Image")]
    [SerializeField] private FillImage healthImage;

    [Header("Normal Text")]
    [SerializeField] private NormalText healthText;
    [SerializeField] private NormalText coinText;
    [SerializeField] private NormalText arrowText;

    [Header("Shop")]
    public GameObject shop;

    [Header("Enchant")]
    public GameObject enchant;

    [Header("EscapeMenu")]
    public GameObject escape;
    public bool isEscapeOpen = false;

    [Header("DieMenu")]
    public GameObject die;

    [Header("ClearMenu")]
    public GameObject clear;

    [Header("Text")]
    [SerializeField] private GameObject textObject;

    public void SetHealthImage(float min, float max)
    {
        healthText.SetText($"{min} / {max}");
        healthImage.SetImageFillAmount(min, max);
    }

    public void SetCoinText(string text)
    {
        coinText.SetText(text);
    }

    public void SetArrowText(string text)
    {
        arrowText.SetText(text);
    }

    public void EnableShopPanel(bool isActive)
    {
        shop.SetActive(isActive);
    }

    public void EnableEnchantPanel(bool isActive)
    {
        enchant.SetActive(isActive);
    }

    public void InstantiateTextObject(string text)
    {
        if (this.textObject == null) return;

        GameObject textObject = Instantiate(this.textObject, new Vector3(960.0f, 180.0f, 0.0f), Quaternion.identity, transform);
        textObject.GetComponent<NormalText>().SetText(text);
    }

    private void Update()
    {
        OpenEscape();
    }

    private void OpenEscape()
    {
        isEscapeOpen = escape.activeSelf;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isEscapeOpen == false)
                escape.SetActive(true);
            else
                escape.SetActive(false);
        }
    }
}
