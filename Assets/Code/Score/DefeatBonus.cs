using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatBonus : MonoBehaviour
{
    // 得点
    [Header("得点")]
    [SerializeField] int Point;


    private void OnDestroy()
    {
        BlockParameter blockParameter = GetComponent<BlockParameter>();
        // このブロックがもし耐久値が０になることで破壊されたならスコア加算
        if (
            blockParameter != null &&
            blockParameter.isDestroy()
            )
        {
            ServiceLocator.Locator.GetScoreManager().AddScore(Point);
        }
    }
}
