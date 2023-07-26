using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    // 基本の位置
    public Vector3 DefaultPos { private get; set; }
    // 振動の角度
    public float Angle { private get; set; }
    // 振れ幅
    public float Width { private get; set; }
    // 振動時間
    public float ShackTime { private get; set; }
    // 振動速度(振動回数/秒)
    public float Speed { private get; set; }
    // 残り振動時間
    private float m_shakeLimit;

    private void Awake()
    {
        m_shakeLimit = 0;
    }

    private void FixedUpdate()
    {
        // 振動していたら;
        if (m_shakeLimit > 0)
        {
            /// 描画場所がどこか計算
            // 振動した時間を計算
            float nowTime = ShackTime - m_shakeLimit;
            // 現在の振動回数を計算
            int ShakeNum = (int)(nowTime * Speed);
            Debug.Log(ShakeNum);
            // 移動分の計算
            float rad = Angle * Mathf.Deg2Rad;
            Vector3 vec = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
            vec *= Width;
            Debug.Log(vec);
            // 振動回数が奇数か偶数か調べる(0は偶数とする)
            if (ShakeNum % 2 == 0)
            {
                /// 偶数だったら
                // 移動分をそのまま加算
                transform.position = DefaultPos + vec;
                Debug.Log("Normal");
            }
            else
            {
                /// 奇数だったら
                // 偶数とは逆向きに移動
                transform.position = DefaultPos - vec;
                Debug.Log("Reverse");
            }

            // 時間を減らす
            m_shakeLimit -= Time.fixedDeltaTime;
        }
        // 振動していなければ
        else
        {
            // 規定値を適用
            transform.position = DefaultPos;
        }
    }

    // 振動させる
    public void Play()
    {
        m_shakeLimit = ShackTime;
    }
}
