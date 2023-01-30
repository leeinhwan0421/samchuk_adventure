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
        talkData.Add(10, new string[] { "삐약" });

        talkData.Add(100, new string[] { "'J' 키를 눌러 공격 할 수 있습니다." });
        talkData.Add(200, new string[] { "엘리베이터를 타고 내려 갈 수 있습니다." });
        talkData.Add(300, new string[] { "앞으로 가면 더 이상 돌아갈 수 없을 것 같다."});
        talkData.Add(400, new string[] { "[ 삼척 동양 광산 ]" });
        talkData.Add(500, new string[] { "화살이 없으면 활을 쏘지 못한다", "화살 10발은 기본적으로 가지고 있다."});
        talkData.Add(600, new string[] { "숨겨진 레버를 당겨야 한다." });
        talkData.Add(999, new string[] { "You have to live a beautiful life forever" });

        talkData.Add(1000, new string[] { "안녕?", "이 곳에 처음 왔구나?" });
        talkData.Add(2000, new string[] { "뭐라도 하나 사줄꺼지?" });
        talkData.Add(3000, new string[] { "뭐라도 하나 강화해봐!" });
    }

    public string GetTalk(int id, int talkIndex)
    {
        // 딕셔너리 키 찾는 함수
        if (!talkData.ContainsKey(id))
        {
            if (!talkData.ContainsKey(id - id % 10))
            {
                // Quest 대사 부터 없을 때
                // 기본 대사 return
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
