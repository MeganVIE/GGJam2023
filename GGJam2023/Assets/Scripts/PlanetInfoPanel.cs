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

    }

    private void OkHandler()
    {

    }
}