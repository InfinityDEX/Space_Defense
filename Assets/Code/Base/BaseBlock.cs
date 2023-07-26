using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBlock : MonoBehaviour
{
    private void OnDestroy()
    {
        ServiceLocator.Locator.GetSystem().GameOver(3);
    }
}
