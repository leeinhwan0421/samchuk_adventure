using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class U_Text : MonoBehaviour
{
    private string textStr = null;
    private int index = 0;

    public Text text;

    public bool isTyping = true;

    public void SetText(string textStr)
    {
        this.textStr = textStr;

        this.text.text = "";
        index = 0;

        isTyping = false;

        Typing();
    }

    private void Typing()
    {
        this.text.text += textStr[index];

        index++;

        if (this.text.text != textStr)
            Invoke(nameof(Typing), 0.05f);
        else
            EndTyping();
    }

    private void EndTyping()
    {
        isTyping = true;
    }
}
