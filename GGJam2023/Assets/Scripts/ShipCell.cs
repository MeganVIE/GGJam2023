using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipCell : MonoBehaviour
{
    [SerializeField] private Image m_icon;
    [SerializeField] private TextMeshProUGUI m_amountText;
    [SerializeField] private ResourceType m_type = ResourceType.None;
    [SerializeField] private int m_amount = 0;
    [SerializeField] private Button m_btn;

    public int Amount => m_amount;
    public ResourceType Type => m_type;

    public event Action<ShipCell> OnCellClick;

    private void OnEnable()
    {
        m_btn.onClick.AddListener(() => OnCellClick?.Invoke(this));
    }

    private void OnDisable()
    {
        m_btn.onClick.RemoveAllListeners();
    }

    public void ChangeResource(int value = 0)
    {
        m_amount += value;
        m_amountText.text = m_amount.ToString();

        if (m_amount <= 0)
        {
            m_icon.sprite = null;
            m_amountText.text = String.Empty;
        }
    }

    public void SetResource(Sprite sprite, ResourceType type)
    {
        m_icon.sprite = sprite;
        m_type = type;
    }
}