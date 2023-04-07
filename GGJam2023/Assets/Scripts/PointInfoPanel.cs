using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointInfoPanel : MonoBehaviour
{
    [SerializeField] private Ship m_ship;
    [SerializeField] private Button m_btn;
    [SerializeField] private ResourceInfoPanel m_oxygenInfo;
    [SerializeField] private ResourceInfoPanel m_energyInfo;
    [SerializeField] private ResourceInfoPanel m_healthInfo;
    [SerializeField] private TextMeshProUGUI m_pointName;

    private Point m_point;

    private void Awake()
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_btn.onClick.AddListener(OnClickHandler);
    }

    private void OnDisable()
    {
        m_btn.onClick.RemoveListener(OnClickHandler);
        m_point = null;
    }

    private void OnClickHandler()
    {
        m_ship.FlyToPoint(m_point);

        Hide();
    }

    private void InfoViewUpdate()
    {
        m_oxygenInfo.SetValue(m_point.OxygenSpend);
        m_energyInfo.SetValue(m_point.EnergySpend);
        m_healthInfo.SetValue(m_point.HealthSpend);
        m_pointName.text = m_point.PointName;
    }

    public void Show(Point point)
    {
        gameObject.SetActive(true);
        m_btn.interactable = point != m_ship.CurrentPoint;

        m_point = point;

        InfoViewUpdate();
    }
}