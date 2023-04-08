using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private Button m_okBtn;
    [SerializeField] private ResourceInfoPanel m_alloyOreInfo;
    [SerializeField] private ResourceInfoPanel m_gasInfo;
    [SerializeField] private ResourceInfoPanel m_crystalInfo;
    [SerializeField] private ResourceInfoPanel m_biomaterialInfo;
    [SerializeField] private ResourceInfoPanel m_oxygenInfo;
    [SerializeField] private ResourceInfoPanel m_energyInfo;
    [SerializeField] private ResourceInfoPanel m_healthInfo;
    [SerializeField] private TextMeshProUGUI m_disc;

    private LocalPoint m_point;
    private RandomEventDataSO m_eventData;
    public event Action<LocalPoint> OnOkClick;
    public event Action<RandomEventDataSO> OnEventOkClick;

    private void OnEnable()
    {
        m_okBtn.onClick.AddListener(OkHandler);
    }

    private void OnDisable()
    {
        m_okBtn.onClick.RemoveListener(OkHandler);
    }

    private void OkHandler()
    {
        if (m_point != null)
            OnOkClick?.Invoke(m_point);
        else
            OnEventOkClick?.Invoke(m_eventData);

        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        m_point = null;
        m_eventData = null;
    }

    private void ClearView()
    {
        m_biomaterialInfo.SetValue(0);
        m_crystalInfo.SetValue(0);
        m_alloyOreInfo.SetValue(0);
        m_gasInfo.SetValue(0);
        m_oxygenInfo.SetValue(0);
        m_energyInfo.SetValue(0);
        m_healthInfo.SetValue(0);
    }

    public void Show(LocalPointQuestDataSO questData, LocalPoint point)
    {
        gameObject.SetActive(true);
        m_point = point;
        m_disc.text = questData.Result;
        ClearView();

        foreach (var data in questData.ResourceDatas)
        {
            switch (data.Type)
            {
                case ResourceType.Biomaterials:
                    m_biomaterialInfo.SetValue(data.Value);
                    break;
                case ResourceType.Crystals:
                    m_crystalInfo.SetValue(data.Value);
                    break;
                case ResourceType.AlloyOre:
                    m_alloyOreInfo.SetValue(data.Value);
                    break;
                case ResourceType.Gas:
                    m_gasInfo.SetValue(data.Value);
                    break;
            }
        }
    }

    public void Show(RandomEventDataSO eventData)
    {
        gameObject.SetActive(true);
        m_point = null;
        m_eventData = eventData;
        m_disc.text = eventData.Text;
        ClearView();

        foreach (var data in eventData.ResourceDatas)
        {
            switch (data.Type)
            {
                case ResourceType.Biomaterials:
                    m_biomaterialInfo.SetValue(data.Value);
                    break;
                case ResourceType.Crystals:
                    m_crystalInfo.SetValue(data.Value);
                    break;
                case ResourceType.AlloyOre:
                    m_alloyOreInfo.SetValue(data.Value);
                    break;
                case ResourceType.Gas:
                    m_gasInfo.SetValue(data.Value);
                    break;
            }
        }

        foreach (var data in eventData.ShipStateDatas)
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