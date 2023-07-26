using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEffect : MonoBehaviour
{
    [SerializeField] ShakeEffect m_shakeEffect;
    [SerializeField] BlockParameter m_block;
    // Start is called before the first frame update
    void Start()
    {
        m_shakeEffect.DefaultPos = transform.position;
        m_shakeEffect.Angle = 90;
        m_shakeEffect.Width = 0.1f;
        m_shakeEffect.ShackTime = 0.1f;
        m_shakeEffect.Speed = 60;
    }

    void FixedUpdate()
    {
        if(m_block.Attacked)
        {
            m_shakeEffect.Play();
        }
    }
}
