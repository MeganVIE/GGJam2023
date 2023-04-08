using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SceneController : MonoBehaviour
{
    [Header("Global space")]
    [SerializeField] private GameObject m_globalSpace;
    [SerializeField] private List<Point> m_globalPoints;
    [SerializeField] private PointInfoPanel m_pointPanel;

    [Header("Local space")]
    [SerializeField] private Sprite[] m_localSprites;
    [SerializeField] private Transform[] m_localPointTrs;
    [SerializeField] private LocalPoint[] m_localPointPrefabs;
    [SerializeField] private LocalPointQuestDataSO[] m_questdatas;
    [Space]
    [SerializeField] private GameObject m_localSpace;
    [SerializeField] private SpriteRenderer m_localBackground;
    [SerializeField] private PlanetInfoPanel m_planetPanel;
    [SerializeField] private ResultPanel m_resultPlanet;

    [Header("Other")]
    [SerializeField] private Button m_globalBtn;
    [SerializeField] private Button m_localBtn;
    [SerializeField] private Button m_shipBtn;
    [Space]
    [SerializeField] private ShipStorage m_storage;
    [SerializeField] private Ship m_ship;

    private Dictionary<Point, Sprite> m_localBackgrounds;
    private Dictionary<Point, LocalPoint[]> m_localPoints;
    private Dictionary<Point,Dictionary<LocalPoint, LocalPointQuestDataSO>> m_localquestDatas;

    private void Start()
    {
        m_localBackgrounds = new Dictionary<Point, Sprite>();
        m_localPoints = new Dictionary<Point, LocalPoint[]>();
        m_localquestDatas = new Dictionary<Point,Dictionary<LocalPoint, LocalPointQuestDataSO>>();

        foreach (var lp in m_localPointPrefabs)
        {
            lp.gameObject.SetActive(false);
        }

        foreach (var point in m_globalPoints)
        {
            point.OnClicked = () => OnGlobalPointClickHandler(point);
            m_localBackgrounds.Add(point, m_localSprites[Random.Range(0, m_localSprites.Length)]);

            m_localquestDatas.Add(point,new Dictionary<LocalPoint, LocalPointQuestDataSO>());
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
                if(localPoint.Quest==null)
                    localPoint.SetQuest(m_questdatas[Random.Range(0, m_questdatas.Length)]);
                m_localquestDatas[point].Add(localPoint, localPoint.Quest);
            }
        }

        OnGlobalBtnClickHandler();
        Fly(m_ship.CurrentPoint);
        m_pointPanel.gameObject.SetActive(false);
        m_planetPanel.gameObject.SetActive(false);
        m_resultPlanet.gameObject.SetActive(false);

        m_ship.OnEnergyOut += GameOver;
        m_ship.OnOxygenOut += GameOver;
        m_ship.OnHealthOut += GameOver;
        m_pointPanel.OnFlyClicked += Fly;
        m_planetPanel.OnOkClick += StartQuest;
    }

    private void OnDestroy()
    {
        m_globalPoints.ForEach(p => p.OnClicked = null);

        m_ship.OnEnergyOut -= GameOver;
        m_ship.OnOxygenOut -= GameOver;
        m_ship.OnHealthOut -= GameOver;
        m_pointPanel.OnFlyClicked -= Fly;
        m_planetPanel.OnOkClick -= StartQuest;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void OnGlobalPointClickHandler(Point point)
    {
        if (m_ship.CurrentPoint != point)
            m_pointPanel.Show(point, m_ship.CurrentPoint);
    }

    private void OnLocalPointClickHandler(LocalPoint point)
    {
        m_planetPanel.Show(m_localquestDatas[m_ship.CurrentPoint][point], point);
    }

    private void StartQuest(LocalPoint point)
    {
        m_ship.SpendStates(point);
        m_resultPlanet.Show(m_localquestDatas[m_ship.CurrentPoint][point], point);
        m_resultPlanet.OnOkClick += GetResources;
    }

    private void GetResources(LocalPoint point)
    {
        m_resultPlanet.OnOkClick -= GetResources;
        m_storage.SetResources(m_localquestDatas[m_ship.CurrentPoint][point].ResourceDatas);
    }

    private void Fly(Point point)
    {
        for (int i = 0; i < 3; i++)
        {
            m_localPoints[m_ship.CurrentPoint][i].gameObject.SetActive(false);
            m_localPoints[m_ship.CurrentPoint][i].OnClicked = null;

            var newPoint = m_localPoints[point][i];
            newPoint.gameObject.SetActive(true);
            newPoint.transform.position = m_localPointTrs[i].position;
            newPoint.OnClicked = () => OnLocalPointClickHandler(newPoint);
        }

        m_localBackground.sprite = m_localBackgrounds[m_ship.CurrentPoint];
        m_ship.FlyToPoint(point);
    }

    public void OnGlobalBtnClickHandler()
    {
        m_globalSpace.SetActive(true);
        m_localSpace.SetActive(false);
        m_storage.Hide();

        m_globalBtn.interactable = false;
        m_localBtn.interactable = true;
        m_shipBtn.interactable = true;
    }

    public void OnLocalBtnClickHandler()
    {
        m_globalSpace.SetActive(false);
        m_localSpace.SetActive(true);
        m_storage.Hide();
        m_pointPanel.Hide();

        m_globalBtn.interactable = true;
        m_localBtn.interactable = false;
        m_shipBtn.interactable = true;
    }

    public void OnShipBtnClickHandler()
    {
        m_globalSpace.SetActive(false);
        m_localSpace.SetActive(false);
        m_storage.Show();
        m_pointPanel.Hide();

        m_globalBtn.interactable = true;
        m_localBtn.interactable = true;
        m_shipBtn.interactable = false;
    }
}