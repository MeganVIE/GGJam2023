using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private Button m_btn;

    private Dictionary<EndType, string> m_endTexts = new Dictionary<EndType, string>
    {
        [EndType.Good] = "Вы молодец!",
        [EndType.Energy] = "Энергия закончилась, а вместе с ней и ваше приключение. Остается надеяться, что ваш корабль когда-нибудь найду ваши сородичи.",
        [EndType.Oxygen] = "Кислород закончился, как и ваша жизнь. Жаль, что вы не добрались до своей родины...",
        [EndType.Health] = "Корабль уничтожен, как и ваша надежда увидеть свой дом..."
    };

    private void OnEnable()
    {
        m_btn.onClick.AddListener(() => SceneManager.LoadScene(0));
    }

    private void OnDisable()
    {
        m_btn.onClick.RemoveAllListeners();
    }

    public void Show(EndType type)
    {
        gameObject.SetActive(true);
        m_text.text = m_endTexts[type];
    }
}

public enum EndType
{
    Good,
    Energy,
    Oxygen,
    Health
}