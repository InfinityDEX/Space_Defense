using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // ゲームオーバートリガー
    public bool isGameOver { get; private set; }
    // シーン名
    [SerializeField] string m_result;
    // 遅延シーン移動時間
    private float m_endLimit;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    void FixedUpdate()
    {
        // ゲームオーバーなら
        if(this.isGameOver && m_endLimit <= 0)
        {
            // リザルトシーンを呼び出す
            SceneManager.LoadScene(m_result);
        }
        m_endLimit -= Time.fixedDeltaTime;
    }

    // ゲームオーバー
    public void GameOver(float limit = 0.0f)
    {
        // 遅延値
        m_endLimit = limit;
        // ゲームオーバーにする
        isGameOver = true;
    }
}
