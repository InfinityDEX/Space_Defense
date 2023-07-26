using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // 得点の総合計
    public static int TotalScore { get; private set; }

    private void Awake()
    {
        TotalScore = 0;
    }

    // 得点を加算する
    public void AddScore(int point)
    {
        TotalScore += point;
    }
}
