using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoPanel : MonoBehaviour
{
    [SerializeField] private Button m_cancelBtn;
    [SerializeField] private Button m_okBtn;
    [SerializeField] private ResourceInfoPanel m_oxygenInfo;
    [SerializeField] private ResourceInfoPanel m_energyInfo;
    [SerializeField] private ResourceInfoPanel m_healthInfo;
    [SerializeField] private TextMeshProUGUI m_planetName;
    [SerializeField] private TextMeshProUGUI m_planetDisc;

    private LocalPoint m_point;
    public event Action<LocalPoint> OnOkClick;

    private void OnEnable()
    {
        m_okBtn.onClick.AddListener(OkHandler);
        m_cancelBtn.onClick.AddListener(CancelHandler);
    }

    private void OnDisable()
    {
        m_okBtn.onClick.RemoveListener(OkHandler);
        m_cancelBtn.onClick.RemoveListener(CancelHandler);
    }

    private void CancelHandler()
    {
        Hide();
    }

    private void OkHandler()
    {
        OnOkClick?.Invoke(m_point);
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        m_point = null;
    }

    public void Show(LocalPointQuestDataSO questData, LocalPoint point)
    {
        gameObject.SetActive(true);
        m_point = point;
        m_planetName.text = point.PointName;
        m_planetDisc.text = questData.Dicription;

        foreach (var data in questData.ShipStateDatas)
        {
            switch (data.Type)
            {
                case ShipStateType.Energy:
                    m_energyInfo.SetValue(data.Value);
                    break;
                case ShipStateType.Health:
                    m_healthInfo.SetValue(data.Value);
                    break;
                case ShipStateType.Oxygen:
                    m_oxygenInfo.SetValue(data.Value);
                    break;
            }
        }
    }
}