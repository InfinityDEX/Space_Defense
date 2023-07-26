using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockParameter : MonoBehaviour
{
    // 破壊できるか？
    [SerializeField] public bool CanDestroy;
    // 移動できるか？
    [SerializeField] public bool CanMovement;
    // 勢力
    public enum Team
    {
        // 連合軍
        Union,
        // 帝国軍
        Empire,
    }
    // シールド属性
    [SerializeField] public bool Shield = false;
    // どの勢力か？
    [SerializeField] Team m_team;
    // 耐久値
    [SerializeField] int m_durable;
    private int m_startDurable;
    // ダメージを受けたときの音
    [SerializeField] AudioClip m_damageSound;
    // 攻撃を受けた
    private bool DelayAttacked; 
    public bool Attacked { get; private set; }
    // フィールドマネージャ
    public FieldManager FieldManager { private get; set; }
    // 移動処理クラス
    public MovementObject moveObject;
    // フィールド座標
    public Vector2Int Position { get; private set; }

    private void Awake()
    {
        // 初期化
        Attacked = false;
        m_startDurable = m_durable;
    }

    private void Start()
    {
        // 移動処理クラスを登録
        moveObject = GetComponent<MovementObject>();
    }

    private void FixedUpdate()
    {

        // ダメージ状態を解除
        if(Attacked == true)
        {
            // ダメージ状態を初期化
            Attacked = false;
        }
        // 遅らせてダメージ状態を適用
        if(DelayAttacked == true)
        {
            DelayAttacked = false;
            Attacked = true;
        }
    }

    // フィールド座標を設定する
    public void SetPosition(int x, int y)
    {
        SetPosition(new Vector2Int(x, y));
    }
    public void SetPosition(Vector2Int pos)
    {
        // 座標を設定する
        Position = pos;
    }

    // 移動する
    public void Move(int x, int y, int power)
    {
        Move(new Vector2Int(x, y), power);
    }

    // 所属を伝える
    public Team GetTeam()
    {
        return m_team;
    }

    // 移動する
    public bool Move(Vector2Int pos, int power)
    {
        // 移動可能でなければ処理しない
        if (!moveObject.CanMove())
            // 移動失敗
            return false;
        // アドレスの住所順は上下がゲーム内と逆なのでひっくり返す
        var address = new Vector2Int(pos.x, -pos.y);
        // 移動を試みる
        var result = FieldManager.MoveBlock(Position, Position + address, power);
        // 結果から処理を変える
        switch (result)
        {
            // 成功した
            case FieldManager.MoveInfo.SUCCESS:
                // 空間上のブロックを移動させる
                moveObject.Move(pos);
                // アドレスを変更する
                Position += address;
                // 移動成功
                return true;
            // 移動先にブロックがあった
            case FieldManager.MoveInfo.USED:
                // TODO:衝突音を鳴らす
                // 移動失敗
                return false;
            // フィールドの外だった
            case FieldManager.MoveInfo.OUTSIDE:
                // TODO:無効時の効果音を鳴らす
                // 移動失敗
                return false;
        }
        // 移動に失敗している
        return false;
    }
    // 破壊されたか？
    public bool isDestroy()
    {
        // 演算結果
        bool result;

        // 破壊出来て、耐久値が０以下だったら破壊
        if(CanDestroy && m_durable <= 0)
            result = true;
        // そうでなければ破壊しない
        else
            result = false;

        // 結果を返す
        return result;
    }

    // ダメージを受ける
    public void Damage()
    {
        // 耐久値を一つ減らす
        m_durable--;

        // 音を鳴らす
        var audio = ServiceLocator.Locator.GetAudio();
        audio.PlayOneShot(m_damageSound, 0.5f);

        // ダメージ状態にする
        DelayAttacked = true;
    }

    // 体力残量を割合で取得
    public float GetLifeRate()
    {
        return (float)m_durable / (float)m_startDurable;
    }
}
