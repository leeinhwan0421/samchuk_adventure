using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    // key, Line 
    Dictionary<int, string[]> talkData;

    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();

        GenerateData();
    }

    private void GenerateData()
    {
        // Talk Data
        // Brown == 1000 * n
        // Sign == 100 * n
        // dummy = 10 + n
        talkData.Add(10, new string[] { "�߾�" });

        talkData.Add(100, new string[] { "'J' Ű�� ���� ���� �� �� �ֽ��ϴ�." });
        talkData.Add(200, new string[] { "���������͸� Ÿ�� ���� �� �� �ֽ��ϴ�." });
        talkData.Add(300, new string[] { "������ ���� �� �̻� ���ư� �� ���� �� ����."});
        talkData.Add(400, new string[] { "[ ��ô ���� ���� ]" });
        talkData.Add(500, new string[] { "ȭ���� ������ Ȱ�� ���� ���Ѵ�", "ȭ�� 10���� �⺻������ ������ �ִ�."});
        talkData.Add(600, new string[] { "������ ������ ��ܾ� �Ѵ�." });
        talkData.Add(999, new string[] { "You have to live a beautiful life forever" });

        talkData.Add(1000, new string[] { "�ȳ�?", "�� ���� ó�� �Ա���?" });
        talkData.Add(2000, new string[] { "���� �ϳ� ���ٲ���?" });
        talkData.Add(3000, new string[] { "���� �ϳ� ��ȭ�غ�!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        // ��ųʸ� Ű ã�� �Լ�
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                // Quest ��� ���� ���� ��
                // �⺻ ��� return
                if (talkIndex == talkData[id - id % 100].Length)
                    return null;
                else
                    return talkData[id - id % 100][talkIndex];
            }
            else
            {
                if (talkIndex == talkData[id - id % 10].Length)
                    return null;
                else
                    return talkData[id - id % 10][talkIndex];
            }
        }

        if (talkIndex == talkData[id].Length)
            return null;
        else
            return talkData[id][talkIndex];
    }
}
