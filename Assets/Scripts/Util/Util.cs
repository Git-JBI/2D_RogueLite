using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 게임 내에서 어디서나 쓸 수 있는 편의 함수
/// </summary>
public static class Util
{
    public static void Shuffle<T>(List<T> list)
    {
        // 리스트의 내용물이 없을 경우 리턴
        if (list.Count == 0)
            return;

        for(int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);

            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
