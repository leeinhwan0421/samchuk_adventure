using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    public Image icon;
    public Image progressBar;

    private void FixedUpdate()
    {
        icon.rectTransform.Rotate(Vector3.forward * Time.fixedDeltaTime * 180.0f);
    }
}
