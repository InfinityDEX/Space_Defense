using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeGage : MonoBehaviour
{
    [SerializeField] private BlockParameter m_parameter;
    private Slider m_slider;
    // Start is called before the first frame update
    void Start()
    {
        m_slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // ライフバーの長さを更新する
        m_slider.value = m_parameter.GetLifeRate();
    }
}
