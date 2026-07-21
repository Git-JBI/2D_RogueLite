using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    // 정수(int)용 확장 메서드
    public static string ToAbbreviatedString(this int value)
    {
        return ((long)value).ToAbbreviatedString();
    }

    // 큰 숫자(long)용 확장 메서드
    public static string ToAbbreviatedString(this long value)
    {
        // 음수 처리
        bool isNegative = value < 0;
        long absValue = (long)Mathf.Abs(value);

        string result;

        if (absValue >= 1000000000) // 1B (10억)
        {
            result = (absValue / 1000000000f).ToString("F2") + "B";
        }
        else if (absValue >= 1000000) // 1M (100만)
        {
            result = (absValue / 1000000f).ToString("F2") + "M";
        }
        else if (absValue >= 1000) // 1K (1000)
        {
            result = (absValue / 1000f).ToString("F2") + "K";
        }
        else
        {
            result = absValue.ToString(); // 1000 미만은 그대로 표시
        }

        return isNegative ? "-" + result : result;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        Util.Shuffle(list);
    }
}