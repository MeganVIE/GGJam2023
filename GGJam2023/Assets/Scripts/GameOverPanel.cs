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
        [EndType.Good] = "Вы добрались до точки назначения, но что-то было не так. Вместо небольшой человеческой колонии здесь были целые города. Вместо людей их населяли пришельцы, отдаленно напоминающие рептилий. Ваш корабль зашел на посадку, а в доке вас уже ожидали. Небольшая делегация обступила вас и один из них взял слово: - Добро пожаловать домой. Как все прошло? Груз в порядке? Никто не проснулся по пути?",
        [EndType.Energy] = "Энергия закончилась, а вместе с ней и ваше приключение. Кораблю остается только дрейфовать в необъятном, но таком родном вам, космосе.",
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