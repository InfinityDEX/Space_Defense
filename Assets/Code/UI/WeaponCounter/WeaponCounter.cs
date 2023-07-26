using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCounter : MonoBehaviour
{
    // ショット砲の個数を表示する
    [SerializeField] private Text m_shotNum;
    // レーザー砲の個数を表示する
    [SerializeField] private Text m_laserNum;
    // シールドの個数を表示する
    [SerializeField] private Text m_shieldNum;

    void FixedUpdate()
    {
        m_shotNum.text = "×" + TotalisationManager.ShotNum;
        m_laserNum.text = "×" + TotalisationManager.LazerNum;
        m_shieldNum.text = "×" + TotalisationManager.ShieldNum;
    }
}
