using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    // シングルトン
    public static ServiceLocator Locator { get; private set; }

    /// 共有アイテム
    // サウンドマネージャ
    [SerializeField] AudioSource AudioSource;
    // システムマネージャー
    [SerializeField] GameSystem GameSystem;
    // 自兵器数マネージャー
    [SerializeField] TotalisationManager Totalisation;
    // スコアマネージャー
    [SerializeField] ScoreManager ScoreManager;

    void Awake()
    {
        if (Locator == null)
            Locator = this;
        else
            Debug.LogError("サービスロケータ ヲ”2回”生成シタナッッ！！");
    }

    // オーディオソースを渡す
    public AudioSource GetAudio()
    {
        return AudioSource;
    }

    // ゲームシステムを渡す
    public GameSystem GetSystem()
    {
        return GameSystem;
    }

    // 兵数マネージャーを渡す
    public TotalisationManager GetTotalisation()
    {
        return Totalisation;
    }

    // スコアマネージャーを渡す
    public ScoreManager GetScoreManager()
    {
        return ScoreManager;
    }
}
