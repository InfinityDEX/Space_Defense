using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // スコアボード
    private Text m_scoreBoard;

    private void Awake()
    {
        m_scoreBoard = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_scoreBoard.text = ScoreManager.TotalScore.ToString();   
    }
}
