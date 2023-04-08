using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePanel : MonoBehaviour
{
    [SerializeField] private List<ResourcePanelDataSO> m_datas;
    [Space]
    [SerializeField] private Button m_closeBtn;
    [SerializeField] private Button m_deleteBtn;
    [SerializeField] private TextMeshProUGUI m_nameText;
    [SerializeField] private TextMeshProUGUI m_discText;
    [SerializeField] private Button m_useBtn;

    public event Action OnDeleteClick;
    public event Action OnUseClick;

    private void OnEnable()
    {
        m_closeBtn.onClick.AddListener(Hide);
        m_deleteBtn.onClick.AddListener(() => OnDeleteClick?.Invoke());
        m_useBtn.onClick.AddListener(() => OnUseClick?.Invoke());
    }

    private void OnDisable()
    {
        m_closeBtn.onClick.RemoveAllListeners();
        m_deleteBtn.onClick.RemoveAllListeners();
        m_useBtn.onClick.RemoveAllListeners();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(ResourceType type)
    {
        if (type == ResourceType.None)
            return;

        gameObject.SetActive(true);

        var data = m_datas.First(d => d.Type == type);
        m_nameText.text = data.ResourceName;
        m_discText.text = data.ResourceDisc;
        m_useBtn.gameObject.SetActive(data.NeedUseButton);
    }
}