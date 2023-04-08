using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipState : MonoBehaviour
{
    [SerializeField] private Image m_valueBar;
    [SerializeField] private TextMeshProUGUI m_maxValue;

    private int m_max;
    private float m_value;

    private void UpdateView()
    {
        var scale = m_valueBar.rectTransform.localScale;
        scale.x = m_value / m_max;
        m_valueBar.rectTransform.localScale = scale;
    }

    public void SetMaxValue(int value)
    {
        m_max = value;
        m_maxValue.text = m_max.ToString();
        UpdateView();
    }

    public void SetCurrentValue(float value)
    {
        m_value = value;
        UpdateView();
    }
}