using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    [Header("Global space")]
    [SerializeField] private GameObject m_globalSpace;
    [SerializeField] private List<Point> m_globalPoints;

    [Header("Local space")]
    [SerializeField] private Sprite[] m_localSprites;
    [SerializeField] private Transform[] m_localPointTrs;
    [SerializeField] private LocalPoint[] m_localPointPrefabs;
    [Space]
    [SerializeField] private GameObject m_localSpace;
    [SerializeField] private SpriteRenderer m_localBackground;

    [Header("Other")]
    [SerializeField] private Button m_globalBtn;
    [SerializeField] private Button m_localBtn;
    [SerializeField] private Button m_shipBtn;
    [Space]
    [SerializeField] private GameObject m_shipSpace;
    [SerializeField] private Ship m_ship;
    [SerializeField] private GameObject m_shipStatesPanel;
    [SerializeField] private PointInfoPanel m_pointPanel;

    private Dictionary<Point, Sprite> m_localBackgrounds;
    private Dictionary<Point, LocalPoint[]> m_localPoints;

    private void Start()
    {
        m_localBackgrounds = new Dictionary<Point, Sprite>();
        m_localPoints = new Dictionary<Point, LocalPoint[]>();

        for (var i = 0; i < m_localPointPrefabs.Length; i++)
        {
            m_localPointPrefabs[i].gameObject.SetActive(false);
        }

        foreach (var point in m_globalPoints)
        {
            point.OnSelect = () => m_pointPanel.Show(point);
            m_localBackgrounds.Add(point, m_localSprites[Random.Range(0, m_localSprites.Length)]);

            m_localPoints.Add(point, new LocalPoint[3]);
            List<LocalPoint> lps = new List<LocalPoint>();
            for (int i = 0; i < 3; i++)
            {
                var localPoint = m_localPointPrefabs[Random.Range(0, m_localPointPrefabs.Length)];
                while (lps.Contains(localPoint))
                {
                    localPoint = m_localPointPrefabs[Random.Range(0, m_localPointPrefabs.Length)];
                }
                lps.Add(localPoint);
                m_localPoints[point][i] = localPoint;
            }
        }

        OnGlobalBtnClickHandler();
        Fly(m_ship.CurrentPoint);

        m_ship.OnEnergyOut += GameOver;
        m_ship.OnOxygenOut += GameOver;
        m_ship.OnHealthOut += GameOver;
        m_pointPanel.OnFlyClicked += Fly;
    }

    private void OnDestroy()
    {
        m_globalPoints.ForEach(p => p.OnSelect = null);

        m_ship.OnEnergyOut -= GameOver;
        m_ship.OnOxygenOut -= GameOver;
        m_ship.OnHealthOut -= GameOver;
        m_pointPanel.OnFlyClicked -= Fly;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void OnLocalPointClickHandler(LocalPoint point)
    {

    }

    private void Fly(Point point)
    {
        for (int i = 0; i < 3; i++)
        {
            m_localPoints[m_ship.CurrentPoint][i].gameObject.SetActive(false);
            m_localPoints[point][i].gameObject.SetActive(true);
            m_localPoints[point][i].transform.position = m_localPointTrs[i].position;
        }

        m_localBackground.sprite = m_localBackgrounds[m_ship.CurrentPoint];
        m_ship.FlyToPoint(point);
    }

    public void OnGlobalBtnClickHandler()
    {
        m_globalSpace.SetActive(true);
        m_localSpace.SetActive(false);
        m_shipSpace.SetActive(false);
        m_shipStatesPanel.SetActive(false);

        m_globalBtn.interactable = false;
        m_localBtn.interactable = true;
        m_shipBtn.interactable = true;
    }

    public void OnLocalBtnClickHandler()
    {
        m_globalSpace.SetActive(false);
        m_localSpace.SetActive(true);
        m_shipSpace.SetActive(false);
        m_shipStatesPanel.SetActive(true);

        m_globalBtn.interactable = true;
        m_localBtn.interactable = false;
        m_shipBtn.interactable = true;
    }

    public void OnShipBtnClickHandler()
    {
        m_globalSpace.SetActive(false);
        m_localSpace.SetActive(false);
        m_shipSpace.SetActive(true);
        m_shipStatesPanel.SetActive(true);

        m_globalBtn.interactable = true;
        m_localBtn.interactable = true;
        m_shipBtn.interactable = false;
    }
}