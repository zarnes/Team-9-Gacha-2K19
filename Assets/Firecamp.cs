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

    public CraftingMenu m_crafting_menu;

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
        if (m_crafting_menu == null)
        {
            Debug.LogError("Crafting menu cannot be null.");
        }
        m_crafting_menu.ToggleMenu();
    }

    void OnMouseOver()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
