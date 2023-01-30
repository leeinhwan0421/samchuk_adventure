using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    [Header("Objects")]
    public GameObject talkPanel;
    public U_Text talkText;
    public GameObject scanObject;

    [Header("Active Value")]
    public bool isAction;

    [Header("Line")]
    public int talkIndex;

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        TalkObj talkData = scanObject.GetComponent<TalkObj>();
        Talk(talkData.id, talkData.isNpc);

        talkPanel.SetActive(isAction);
    }
    
    private void Talk(int id, bool isNpc)
    {
        // 타이핑 중에는 넘어 갈 수 없음...
        // 상점 이용 중에는 넘어 갈 수 없음..
        if (talkText.isTyping == false) return;
        if (GameManager.Instance.uiManager.shop.activeSelf == true) return;

        string line = GameManager.Instance.lineManager.GetTalk(id, talkIndex);

        if (line == null)
        {
            isAction = false;
            talkIndex = 0;

            CheckEventToID(id);

            return;
        }

        if (isNpc)
        {
            talkText.SetText(line);
        }
        else
        {
            talkText.SetText(line);
        }

        isAction = true;
        talkIndex++;
    }

    private void CheckEventToID(int id)
    {
        switch (id)
        {
            case 2000:
                GameManager.Instance.uiManager.EnableShopPanel(true);
                break;
            case 3000:
                GameManager.Instance.uiManager.EnableEnchantPanel(true);
                break;
        }
    }
}
