using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    // 生成する兵器
    [SerializeField] BlockParameter m_block;
    // フィールドマネージャー
    public FieldManager FieldManager { private get; set; }
    // マスの座標
    public Vector2Int MassPoint { private get; set; }
    // 1フレーム前に管理しているマスが踏まれていたか？
    private BlockParameter m_steppedBlock;

    // 初期化
    private void Start()
    {
        m_steppedBlock = null;
    }

    // 更新
    private void FixedUpdate()
    {
        BlockParameter block;
        // 生成マスが踏まれているか確認する
        if (block = FieldManager.CheckBlock(MassPoint))
        {
            // 前のフレームは踏まれていない
            if (
                m_steppedBlock != block &&
                block.GetComponent<Player>() != null
                )
            {
                // 生成座標
                Vector2Int p;
                if (
                    FieldManager.CheckGeneratingPoint(p = MassPoint + Vector2Int.down) || 
                    FieldManager.CheckGeneratingPoint(p = MassPoint + Vector2Int.up)
                    )
                {
                    // ブロック生成
                    CreateBlock(p);
                }
            }
        }
        // このフレームにここを踏んでいたブロックの情報を保持する
        m_steppedBlock = block;
    }

    // ブロックを生成する
    private void CreateBlock(Vector2Int generatePoint)
    {
        if(!FieldManager.CheckBlock(generatePoint))
        {
            // オブジェクト作成
            BlockParameter go = new BlockParameter();
            go = Instantiate(m_block, transform);
            go.transform.parent = FieldManager.gameObject.transform;
            go.transform.localPosition = new Vector3(generatePoint.x, -generatePoint.y);
            go.FieldManager = FieldManager;
            go.SetPosition(generatePoint.x, generatePoint.y);

            FieldManager.RegisterBlock(go, generatePoint.x, generatePoint.y);
        }
    }
}
