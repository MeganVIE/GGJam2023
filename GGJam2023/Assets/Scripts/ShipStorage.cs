using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipStorage : MonoBehaviour
{
    [SerializeField] private List<ShipCell> m_cells;
    [Space]
    [SerializeField] private Sprite m_spriteGas;
    [SerializeField] private Sprite m_spriteAlloyOre;
    [SerializeField] private Sprite m_spriteBiomaterials;
    [SerializeField] private Sprite m_spriteCrystals;
    [SerializeField] private Sprite m_spriteCryocapsule;
    [SerializeField] private Sprite m_spriteEngine;
    [SerializeField] private Sprite m_spritePowerGenerator;
    [SerializeField] private Sprite m_spriteOxygenGenerator;
    [SerializeField] private Sprite m_spriteCabin;

    private const int MAX_SIZE = 10;

    private Dictionary<ResourceType, List<ShipCell>> m_resources;
    private Dictionary<ResourceType, Sprite> m_sprites;

    private void Init()
    {
        m_sprites = new Dictionary<ResourceType, Sprite>
        {
            [ResourceType.Gas] = m_spriteGas,
            [ResourceType.AlloyOre] = m_spriteAlloyOre,
            [ResourceType.Biomaterials] = m_spriteBiomaterials,
            [ResourceType.Crystals] = m_spriteCrystals,
            [ResourceType.Cryocapsule] = m_spriteCryocapsule,
            [ResourceType.Engine] = m_spriteEngine,
            [ResourceType.PowerGenerator] = m_spritePowerGenerator,
            [ResourceType.OxygenGenerator] = m_spriteOxygenGenerator,
            [ResourceType.Cabin] = m_spriteCabin,
        };

        m_resources = new Dictionary<ResourceType, List<ShipCell>>();

        foreach (var cell in m_cells)
        {
            if (!m_resources.ContainsKey(cell.Type))
                m_resources.Add(cell.Type, new List<ShipCell>());
            m_resources[cell.Type].Add(cell);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        foreach (var cell in m_cells)
        {
            cell.ChangeResource();
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetResources(List<Data<ResourceType>> resourceDatas)
    {
        if (m_resources == null)
            Init();
        
        foreach (var data in resourceDatas)
        {
            if (data.Value == 0)
                continue;

            if (!m_resources.ContainsKey(data.Type))
            {
                if (m_resources[ResourceType.None].Count > 0)
                {
                    var cell = m_resources[ResourceType.None][0];
                    cell.SetResource(m_sprites[data.Type], data.Type);
                    m_resources[ResourceType.None].Remove(cell);

                    m_resources.Add(data.Type, new List<ShipCell> {cell});
                }
                else
                    continue;
            }

            int value = data.Value;
            while (value > 0)
            {
                var cell = m_resources[data.Type].FirstOrDefault(c => c.Amount < MAX_SIZE);

                if (cell == null)
                {
                    if (m_resources[ResourceType.None].Count > 0)
                    {
                        cell = m_resources[ResourceType.None][0];
                        cell.SetResource(m_sprites[data.Type], data.Type);
                        m_resources[ResourceType.None].Remove(cell);

                        m_resources[data.Type].Add(cell);
                    }
                    else
                        value = 0;
                }
                else
                {
                    int result = MAX_SIZE - cell.Amount;
                    cell.ChangeResource(value > result ? result : value);
                    value -= result;
                }
            }
        }
    }
}