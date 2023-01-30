using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomManager : MonoBehaviour
{
    public GameObject virtualCam;
    public GameObject directionLight;

    private void Start()
    {
        virtualCam.SetActive(false);
    }

    private void RoomEffect()
    {
        switch (gameObject.name)
        {
            case "BackGroundList1":
                StartCoroutine(SetDirectionLight(1f));
                SoundInstance.Instance.ChangeStageBGM(0);
                break;
            case "BackGroundList2":
                StartCoroutine(SetDirectionLight(0.5f));
                SoundInstance.Instance.ChangeStageBGM(1);
                break;
            case "BackGroundList3":
                StartCoroutine(SetDirectionLight(0.75f));
                SoundInstance.Instance.ChangeStageBGM(2);
                break;
            case "BackGroundList4":
                StartCoroutine(SetDirectionLight(0.66f));
                SoundInstance.Instance.ChangeStageBGM(2);
                break;
        }
    }

    private IEnumerator SetDirectionLight(float intensity)
    {
        Light2D directionLight2D = directionLight.GetComponent<Light2D>();

        float first = directionLight2D.intensity;

        if (first > intensity)
        {
            while (true)
            {
                first -= Time.deltaTime;

                yield return new WaitForEndOfFrame();

                directionLight2D.intensity = first;

                if (first <= intensity)
                {
                    break;
                }
            }
        }

        else if (first < intensity)
        {
            while (true)
            {
                first += Time.deltaTime;

                yield return new WaitForEndOfFrame();

                directionLight2D.intensity = first;

                if (first >= intensity)
                {
                    break;
                }
            }
        }

        directionLight2D.intensity = first;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.SetActive(true);
            RoomEffect();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCam.SetActive(false);
        }
    }
}