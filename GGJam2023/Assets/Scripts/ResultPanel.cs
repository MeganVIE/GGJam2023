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
    [SerializeField] private TextMeshProUGUI m_disc;

    private LocalPoint m_point;
    public event Action<LocalPoint> OnOkClick;

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
        m_disc.text = questData.Result;

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
}