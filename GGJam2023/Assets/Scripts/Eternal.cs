
using UnityEngine;

public class Eternal : MonoBehaviour
{
    private static Eternal m_instance;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            DestroyImmediate(gameObject);
    }
}