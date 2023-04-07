using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Point : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private PointInfoPanel m_infoPanel;
    [SerializeField] private GameObject m_indicator;
    [SerializeField] private GameObject m_selected;
    [Space]
    [SerializeField] private int m_OxygenSpend;
    [SerializeField] private int m_HealthSpend;
    [SerializeField] private int m_EnergySpend;
    [SerializeField] private string m_pointName;

    public int OxygenSpend => m_OxygenSpend;
    public int HealthSpend => m_HealthSpend;
    public int EnergySpend => m_EnergySpend;
    public string PointName => m_pointName;

    public void OnPointerClick(PointerEventData eventData)
    {
        m_infoPanel.Show(this);
        UpdateSelectedStatus(true);
    }

    public void UpdateCurrentStatus(bool value)
    {
        m_indicator.SetActive(value);
    }

    public void UpdateSelectedStatus(bool value)
    {
        m_selected.SetActive(value);
    }
}