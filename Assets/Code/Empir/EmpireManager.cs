using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpireManager : MonoBehaviour
{
    // 隊列の発生までの期間
    [SerializeField] float m_span;
    // 隊列
    List<List<BlockParameter>> m_blockParameters;
    // 帝国軍ジェネレータ
    EmpirGenerator m_empirGenerator;
    // カウンター
    float m_counter;

    // Start is called before the first frame update
    void Start()
    {
        m_empirGenerator = GetComponent<EmpirGenerator>();
        m_blockParameters = new List<List<BlockParameter>>();
        for (int i = 30; i >= 0; i--)
        {
            m_blockParameters.Add(m_empirGenerator.Generate());
            foreach (var hor in m_blockParameters)
            {
                foreach (var block in hor)
                {
                    if (block)
                        block.moveObject.transform.Translate(Vector2.up);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(m_counter <= 0)
            Generate(Vector2Int.down);

        m_counter -= Time.fixedDeltaTime;
    }

    private void Generate(Vector2Int vec)
    {
        m_counter = m_span;

        m_blockParameters.Add(m_empirGenerator.Generate());

        foreach (var hor in m_blockParameters)
        {
            foreach (var block in hor)
            {
                if(block)
                    block.moveObject.Move(vec);
            }
        }
    }
}
