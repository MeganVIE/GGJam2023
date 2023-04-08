using TMPro;
using UnityEngine;

public class ResourceInfoPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_value;

    public void SetValue(int value)
    {
        m_value.text = value.ToString();
        gameObject.SetActive(value != 0);
    }
}