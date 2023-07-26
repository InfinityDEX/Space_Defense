using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalisationManager : MonoBehaviour
{
    // ショット砲の数
    static public int ShotNum { get; private set; }
    // レーザー砲の数
    static public int LazerNum { get; private set; }
    // シールドの数
    static public int ShieldNum { get; private set; }

    private void Awake()
    {
        // 初期化
        ShotNum = LazerNum = ShieldNum = 0;
    }

    // ショット砲の数を増やす
    public static void AddShotNum()
    {
        ShotNum++;
    }
    // ショット砲の数を減らす
    public static void AbateShotNum()
    {
        ShotNum--;
    }

    // レーザー砲の数を増やす
    public static void AddLazerNum()
    {
        LazerNum++;
    }
    // レーザー砲の数を減らす
    public static void AbateLazerNum()
    {
        LazerNum--;
    }

    // シールドの数を増やす
    public static void AddShieldNum()
    {
        ShieldNum++;
    }
    // シールドの数を減らす
    public static void AbateShieldNum()
    {
        ShieldNum--;
    }

}
