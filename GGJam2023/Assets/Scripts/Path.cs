using UnityEngine;

public class Path : MonoBehaviour
{
   [SerializeField] private LineRenderer m_line;
   [Space]
   [SerializeField] private Point m_pointFrom;
   [SerializeField] private Point m_pointTo;

   private void Awake()
   {
      UpdateLine();
   }

   private void OnValidate()
   {
      UpdateLine();
   }

   private void UpdateLine()
   {
      m_line.SetPosition(0, (Vector2)m_pointFrom.transform.position);
      m_line.SetPosition(1, (Vector2)m_pointTo.transform.position);
   }

   public bool CanFly(Point pointFrom, Point pointTo) => (m_pointFrom == pointFrom && m_pointTo == pointTo) ||
                                                         (m_pointFrom == pointTo && m_pointTo == pointFrom);
}