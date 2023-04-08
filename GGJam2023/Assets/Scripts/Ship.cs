using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
   [SerializeField] private Point m_startPoint;
   [SerializeField] private ShipStatesPanel m_statesPanel;
   [Header("Максимальное")]
   [SerializeField] private int m_maxHealth = 100;
   [SerializeField] private int m_maxEnergy = 100;
   [SerializeField] private int m_maxOxygen = 100;
   [Header("Текущее")]
   [SerializeField] private float m_health = 100;
   [SerializeField] private float m_energy = 100;
   [SerializeField] private float m_oxygen = 100;

   private Point m_point;

   public Point CurrentPoint => m_point;
   public float Health => m_health;
   public float Energy => m_energy;
   public float Oxygen => m_oxygen;

   public float MaxHealth => m_maxHealth;
   public float MaxEnergy => m_maxEnergy;
   public float MaxOxygen => m_maxOxygen;

   public bool FullHealth => m_maxHealth - m_health == 0;
   public bool FullEnergy => m_maxEnergy - m_energy == 0;
   public bool FullOxygen => m_maxOxygen - m_oxygen == 0;

   public event Action OnEnergyOut;
   public event Action OnHealthOut;
   public event Action OnOxygenOut;

   private void Awake()
   {
      m_point = m_startPoint;
      m_point.UpdateCurrentStatus(true);

      m_statesPanel.SetMaxValues(m_maxOxygen, m_maxEnergy, m_maxHealth);
      m_statesPanel.UpdateValues(m_oxygen, m_energy, m_health);
   }

   public void FlyToPoint(Point point)
   {
      if (point == CurrentPoint)
         return;

      m_point.UpdateCurrentStatus(false);
      m_point = point;
      m_point.UpdateCurrentStatus(true);

      SpendStates(m_point);
   }

   public void SpendStates(Point point)
   {
      ChangeEnergy(-point.EnergySpend);
      ChangeHealth(-point.HealthSpend);
      ChangeOxygen(-point.OxygenSpend);
   }

   public void ChangeEnergy(float value)
   {
      m_energy += value;

      if (m_energy > m_maxEnergy)
         m_energy = m_maxEnergy;

      if (m_energy <= 0)
      {
         m_energy = 0;
         OnEnergyOut?.Invoke();
      }

      m_statesPanel.UpdateEnergyState(m_energy);
   }

   public void ChangeHealth(float value)
   {
      m_health += value;

      if (m_health > m_maxHealth)
         m_health = m_maxHealth;

      if (m_health <= 0)
      {
         m_health = 0;
         OnEnergyOut?.Invoke();
      }

      m_statesPanel.UpdateHealthState(m_health);
   }

   public void ChangeOxygen(float value)
   {
      m_oxygen += value;

      if (m_oxygen > m_maxOxygen)
         m_oxygen = m_maxOxygen;

      if (m_oxygen <= 0)
      {
         m_oxygen = 0;
         OnEnergyOut?.Invoke();
      }

      m_statesPanel.UpdateOxygenState(m_oxygen);
   }
}