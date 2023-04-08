using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipStorage : MonoBehaviour
{
    [SerializeField] private List<ShipCell> m_cells;
    [SerializeField] private ResourcePanel m_panel;
    [SerializeField] private Ship m_ship;
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

    private void Start()
    {
        Init();
    }

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

            cell.OnCellClick += OnCellClickHandler;
        }

        m_panel.OnDeleteClick += OnDeleteClickHandler;
        m_panel.OnUseClick += OnUseClickHandler;
    }

    private void OnCellClickHandler(ShipCell cell)
    {
        m_panel.Show(cell);
    }

    private void OnDeleteClickHandler(ShipCell cell)
    {
        m_resources[cell.Type].Remove(cell);
        cell.ChangeResource(-MAX_SIZE);
    }

    private void OnUseClickHandler(ShipCell cell)
    {
        switch (cell.Type)
        {
            case ResourceType.Cabin:
                if (!m_ship.FullHealth)
                {
                    float needed = m_ship.MaxHealth - m_ship.Health;

                    if (m_resources.ContainsKey(ResourceType.AlloyOre))
                    {
                        var oreCells = new List<ShipCell>(m_resources[ResourceType.AlloyOre]);
                        if (oreCells.Count > 0)
                        {
                            oreCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var oc in oreCells)
                            {
                                var fromCell = oc.Amount * 5;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeHealth(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(oc);
                                }
                                else
                                {
                                    int count = (int)(needed * .2f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeHealth(needed);
                                    oc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }

                    if (m_resources.ContainsKey(ResourceType.Gas))
                    {
                        var gasCells = new List<ShipCell>(m_resources[ResourceType.Gas]);
                        if (gasCells.Count > 0)
                        {
                            gasCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var gc in gasCells)
                            {
                                var fromCell = gc.Amount * 2.5f;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeHealth(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(gc);
                                }
                                else
                                {
                                    int count = (int)(needed * .4f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeHealth(needed);
                                    gc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }
                }
                break;
            case ResourceType.OxygenGenerator:
                if (!m_ship.FullOxygen)
                {
                    float needed = m_ship.MaxOxygen - m_ship.Oxygen;

                    if (m_resources.ContainsKey(ResourceType.Biomaterials))
                    {
                        var oreCells = new List<ShipCell>(m_resources[ResourceType.Biomaterials]);
                        if (oreCells.Count > 0)
                        {
                            oreCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var oc in oreCells)
                            {
                                var fromCell = oc.Amount * 2.5f;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeOxygen(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(oc);
                                }
                                else
                                {
                                    int count = (int)(needed * .4f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeOxygen(needed);
                                    oc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }

                    if (m_resources.ContainsKey(ResourceType.Gas))
                    {
                        var gasCells = new List<ShipCell>(m_resources[ResourceType.Gas]);
                        if (gasCells.Count > 0)
                        {
                            gasCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var gc in gasCells)
                            {
                                var fromCell = gc.Amount * 2.5f;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeOxygen(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(gc);
                                }
                                else
                                {
                                    int count = (int)(needed * .4f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeOxygen(needed);
                                    gc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }
                }
                break;
            case ResourceType.PowerGenerator:
                if (!m_ship.FullEnergy)
                {
                    float needed = m_ship.MaxEnergy - m_ship.Energy;

                    if (m_resources.ContainsKey(ResourceType.Crystals))
                    {
                        var oreCells = new List<ShipCell>(m_resources[ResourceType.Crystals]);
                        if (oreCells.Count > 0)
                        {
                            oreCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var oc in oreCells)
                            {
                                var fromCell = oc.Amount * 5;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeEnergy(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(oc);
                                }
                                else
                                {
                                    int count = (int)(needed * .2f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeEnergy(needed);
                                    oc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }

                    if (m_resources.ContainsKey(ResourceType.Biomaterials))
                    {
                        var gasCells = new List<ShipCell>(m_resources[ResourceType.Biomaterials]);
                        if (gasCells.Count > 0)
                        {
                            gasCells.Sort((c1, c2) => c1.Amount.CompareTo(c2.Amount));

                            foreach (var gc in gasCells)
                            {
                                var fromCell = gc.Amount * 2.5f;

                                if (fromCell <= needed)
                                {
                                    m_ship.ChangeEnergy(fromCell);
                                    needed -= fromCell;
                                    OnDeleteClickHandler(gc);
                                }
                                else
                                {
                                    int count = (int)(needed * .4f);
                                    if (needed % 5 > 0)
                                        count += 1;

                                    m_ship.ChangeEnergy(needed);
                                    gc.ChangeResource(-count);
                                }

                                if (m_ship.FullHealth)
                                    return;
                            }
                        }
                    }
                }
                break;
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
        m_panel.Hide();
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