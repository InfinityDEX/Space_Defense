using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalisationSubject : MonoBehaviour
{
    public enum Weapon
    {
        Shot,
        Lazer,
        Shield
    }

    [SerializeField] Weapon m_weapon;

    // Start is called before the first frame update
    void Start()
    {
        switch (m_weapon)
        {
            case Weapon.Shot:
                TotalisationManager.AddShotNum();
                break;
            case Weapon.Lazer:
                TotalisationManager.AddLazerNum();
                break;
            case Weapon.Shield:
                TotalisationManager.AddShieldNum();
                break;
            default:
                break;
        }
    }

    private void OnDestroy()
    {
        switch (m_weapon)
        {
            case Weapon.Shot:
                TotalisationManager.AbateShotNum();
                break;
            case Weapon.Lazer:
                TotalisationManager.AbateLazerNum();
                break;
            case Weapon.Shield:
                TotalisationManager.AbateShieldNum();
                break;
            default:
                break;
        }
    }
}
