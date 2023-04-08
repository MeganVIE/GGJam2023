using System;
using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointInfoPanel : MonoBehaviour
{
    [SerializeField] private List<Path> m_paths;
    [Space]
    [SerializeField] private Button m_btn;
    [SerializeField] private ResourceInfoPanel m_oxygenInfo;
    [SerializeField] private ResourceInfoPanel m_energyInfo;
    [SerializeField] private ResourceInfoPanel m_healthInfo;
    [SerializeField] private TextMeshProUGUI m_pointName;

    private Point m_point;

    public event Action<Point> OnFlyClicked;

    public void Hide()
    {
        gameObject.SetActive(false);
        m_point.UpdateSelectedStatus(false);
        m_point = null;
    }

    private void OnEnable()
    {
        m_btn.onClick.AddListener(OnClickHandler);
    }

    private void OnDisable()
    {
        m_btn.onClick.RemoveListener(OnClickHandler);
    }

    private void OnClickHandler()
    {
        OnFlyClicked?.Invoke(m_point);
        Hide();
    }

    private void InfoViewUpdate()
    {
        m_oxygenInfo.SetValue(m_point.OxygenSpend);
        m_energyInfo.SetValue(m_point.EnergySpend);
        m_healthInfo.SetValue(m_point.HealthSpend);
        m_pointName.text = m_point.PointName;
    }

    public void Show(Point point, Point currentPoint)
    {
        gameObject.SetActive(true);
        m_btn.interactable = currentPoint != point && m_paths.Any(p => p.CanFly(currentPoint, point));

        m_point?.UpdateSelectedStatus(false);
        m_point = point;
        m_point.UpdateSelectedStatus(true);

        InfoViewUpdate();
    }
}