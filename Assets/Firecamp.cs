using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firecamp : MonoBehaviour
{
    public AnimationCurve m_animationCurve;
    public Light m_pointLight;
    public float m_minLightIntensity;
    public float m_maxLightIntensity;
    public float m_currentTime = 0.0f;

    void Update()
    {
        float lightIntensityDifference = m_maxLightIntensity - m_minLightIntensity;
        m_currentTime = Mathf.Min(m_currentTime + Time.deltaTime, 1.0f);
        m_pointLight.intensity = m_minLightIntensity + lightIntensityDifference * m_animationCurve.Evaluate(m_currentTime);
        if (m_currentTime >= 1.0f)
        {
            m_currentTime = 0.0f;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("click on firecamp");
    }
}
