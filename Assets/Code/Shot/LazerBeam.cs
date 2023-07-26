using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBeam : MonoBehaviour
{
    // レーザーの方向(上下)
    enum Dir
    {
        Up = 1,
        Down = -1,
    }
    [SerializeField] Dir m_dir;
    // レーザー本体
    [SerializeField] GameObject m_body;
    // エフェクト上
    [SerializeField] GameObject m_top;
    // エフェクト下
    [SerializeField] GameObject m_bottom;
    // チャージタイマー
    [SerializeField] float m_charge;
    // レーザーの寿命
    [SerializeField] float m_life;
    // 発射音
    [SerializeField] AudioClip m_lazerSound;
    // 収束粒子エフェクト
    [SerializeField] GameObject m_chargeEffect;
    // レーザー放射済み
    private bool m_executed;

    // Start is called before the first frame update
    void Start()
    {
        // チャージエフェクトを生成
        GameObject effect = Instantiate(m_chargeEffect, transform);
        // チャージエフェクトの場所をずらす
        effect.transform.position += new Vector3(0, ((int)m_dir * 0.5f) - transform.localScale.y * 0.5f, 0);

        m_executed = false;
        // 発射後の判定やエフェクトを無効化する
        m_top.active = false;
        m_body.active = false;
        m_body.GetComponent<BoxCollider2D>().enabled = false;
        m_bottom.active = false;
    }

    private void FixedUpdate()
    {
        // 寿命がゼロなら消滅する
        if(m_life <= 0)
        {
            // 消滅する
            Destroy(this.gameObject);
        }


        // 所属情報を取得
        var team = GetComponent<BulletAffiliation>().GetTeam();

        // 生成された場所からレーザーの方向に向かって伸ばす
        RaycastHit2D[] raycasts = Physics2D.RaycastAll(transform.position, transform.up);

        if (raycasts.Length > 0)
        {
            foreach (var raycast in raycasts)
            {
                if (raycast.collider.GetComponent<BlockParameter>() == null)
                    continue;
                if (raycast.collider.GetComponent<BlockParameter>().Shield && raycast.collider.GetComponent<BlockParameter>().GetTeam() != team)
                {
                    Debug.Log("レイが" + raycast.collider.name + "と接触");
                    float resultlength = Mathf.Abs(transform.position.y - raycast.collider.transform.position.y) - 0.5f;
                    float resultPos = resultlength * 0.5f + (0.5f * (int)m_dir);
                    m_top.transform.position = new Vector3(transform.position.x, raycast.collider.transform.position.y, transform.position.z);
                    m_bottom.transform.position = transform.position + Vector3.up * (0.5f * (int)m_dir);
                    m_body.transform.localScale = new Vector3(1, resultlength, 1);
                    m_body.transform.localPosition = new Vector3(0, resultPos);
                    break;
                }
            }
        }

        // チャージできていなければ
        if (m_charge > 0)
        {
            // チャージ時間を減らす
            m_charge -= Time.fixedDeltaTime;

            // 点滅させる
            m_body.active = !m_body.active;
        }
        else
        {
            // まだレーザーを発射していなければ撃つ
            if(!m_executed)
            {
                // 発射後の判定やエフェクトを有効にする
                m_top.active = true;
                m_body.active = true;
                m_body.GetComponent<BoxCollider2D>().enabled = true;
                m_bottom.active = true;

                // 音を鳴らす
                var audio = ServiceLocator.Locator.GetAudio();
                audio.PlayOneShot(m_lazerSound, 0.5f);

                // 発射済み
                m_executed = true;
            }
            // チャージ済みなら寿命を減らす
            m_life -= Time.fixedDeltaTime;
        }
    }
}
