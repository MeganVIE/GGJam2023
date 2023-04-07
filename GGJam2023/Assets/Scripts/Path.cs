using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
   [SerializeField] private LineRenderer m_line;
   [Space]
   [SerializeField] private Point m_pointFrom;
   [SerializeField] private Point m_pointTo;

   private List<Point> m_paths;

   private void Awake()
   {
      m_paths = new List<Point> {m_pointFrom, m_pointTo};
      UpdateLine();
   }

   private void UpdateLine()
   {
      m_line.SetPosition(0, (Vector2)m_pointFrom.transform.position);
      m_line.SetPosition(1, (Vector2)m_pointTo.transform.position);
   }

   public bool CanFly(Point pointFrom, Point pointTo)
   {
      return m_paths.Contains(pointFrom) && m_paths.Contains(pointTo);
   }
}