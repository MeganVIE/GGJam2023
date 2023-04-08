using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomEvent", menuName = "create " + nameof(RandomEventDataSO))]
public class RandomEventDataSO : ScriptableObject
{
    public string Text;
    public List<Data<ShipStateType>> ShipStateDatas;
    public List<Data<ResourceType>> ResourceDatas;
}