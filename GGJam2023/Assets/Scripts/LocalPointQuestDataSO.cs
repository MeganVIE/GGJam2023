using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "create " + nameof(LocalPointQuestDataSO))]
public class LocalPointQuestDataSO : ScriptableObject
{
    public string Dicription;
    public List<Data<ShipStateType>> ShipStateDatas;
    public string Result;
    public List<Data<ResourceType>> ResourceDatas;


}

[Serializable]
public struct Data<T>
{
    public T Type;
    public int Value;
}

public enum ShipStateType
{
    Oxygen,
    Energy,
    Health
}

public enum ResourceType
{
    Gas,
    AlloyOre,
    Biomaterials,
    Crystals,
    None,
    Cryocapsule,
    Engine,
    PowerGenerator,
    OxygenGenerator,
    Cabin
}