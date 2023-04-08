using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStatesPanel : MonoBehaviour
{
    [SerializeField] private ShipState m_oxygenState;
    [SerializeField] private ShipState m_energyState;
    [SerializeField] private ShipState m_healthState;

    public void SetMaxValues(int oxygen, int energy, int health)
    {
        SetEnergyMaxValue(energy);
        SetOxygenMaxValue(oxygen);
        SetHealthMaxValue(health);
    }

    public void UpdateValues(float oxygen, float energy, float health)
    {
        UpdateEnergyState(energy);
        UpdateOxygenState(oxygen);
        UpdateHealthState(health);
    }

    public void UpdateEnergyState(float value)
    {
        m_energyState.SetCurrentValue(value);
    }

    public void SetEnergyMaxValue(int value)
    {
        m_energyState.SetMaxValue(value);
    }

    public void UpdateHealthState(float value)
    {
        m_healthState.SetCurrentValue(value);
    }

    public void SetHealthMaxValue(int value)
    {
        m_healthState.SetMaxValue(value);
    }

    public void UpdateOxygenState(float value)
    {
        m_oxygenState.SetCurrentValue(value);
    }

    public void SetOxygenMaxValue(int value)
    {
        m_oxygenState.SetMaxValue(value);
    }
}