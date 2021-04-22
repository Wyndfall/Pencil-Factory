using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private GridPrefabVisual gridPrefabVisual;

    public Tilemap tilemap;
    private float nextActionTime = 0.0f;

    [Header("Material Counters")]

    public int energy;
    public int research;
    public int wood;
    public int graphite;

    [Header("Material Changes")]

    public int energyChange;
    public int researchChange;
    public int woodChange;
    public int graphiteChange;

    [Header("Material Storage Limits")]

    public int energyLimit;
    public int researchLimit;
    public int woodLimit;
    public int graphiteLimit;

    [Header("Building Costs")]

    public int solarCost;
    public int windCost;
    public int researchCost;
    public int mineCost;
    public int treesCost;
    public int groundCost;

    [Header("Stored Resource Bars")]

    public BarManager energyBar;
    public BarManager researchBar;
    public BarManager woodBar;
    public BarManager graphiteBar;

    [Header("Large Resources")]

    public int money;
    public TextMeshProUGUI moneyCounter;


    private void Start()
    {
        //create the tilemap
        tilemap = new Tilemap(gridSettings.gridWidth, gridSettings.gridHeight, gridSettings.cellSize, new Vector3(gridSettings.offsetX, gridSettings.offsetY));
        //setup the tilemap
        gridPrefabVisual.Setup(tilemap.GetGrid());

        UpdateBarLimit();
    }

    private void Update()
    {
        //run any code inside this loop every game tick
        if (Time.time > nextActionTime)
        {
            nextActionTime += gridSettings.tickSpeed;

            CountGrid();
            UpdateValues();
            LimitValues();
            UpdateBars();
        }
    }

    public void UpdateMoney()
    {
        moneyCounter.text = "$" + money.ToString();
    }
    public void UpdateBars()
    {
        UpdateMoney();
        //Display Bar Stats
        energyBar.SetValue(energy, energyChange);
        researchBar.SetValue(research, researchChange);
        woodBar.SetValue(wood, woodChange);
        graphiteBar.SetValue(graphite, graphiteChange);
        //Check Max Values for colors
        if (energy >= energyLimit) { energyBar.SetTextColor(new Color(0.945f, 0.341f, 0.294f)); } else { energyBar.SetTextColor(new Color(1f, 1f, 1f)); }
        if (research >= researchLimit) { researchBar.SetTextColor(new Color(0.945f, 0.341f, 0.294f)); } else { researchBar.SetTextColor(new Color(1f, 1f, 1f)); }
        if (wood >= woodLimit) { woodBar.SetTextColor(new Color(0.945f, 0.341f, 0.294f)); } else { woodBar.SetTextColor(new Color(1f, 1f, 1f)); }
        if (graphite >= graphiteLimit) { graphiteBar.SetTextColor(new Color(0.945f, 0.341f, 0.294f)); } else { graphiteBar.SetTextColor(new Color(1f, 1f, 1f)); }
    }

    public void CountGrid()
    {
        energyChange = graphiteChange = researchChange = woodChange = 0;
        List<MapGridObject> mapGridObjectList = tilemap.GetMapGridObjectList();
        foreach (MapGridObject mapGridObject in mapGridObjectList)
        {
            switch (mapGridObject.GetGridType())
            {
                case MapGridObject.Type.Empty:
                    break;
                case MapGridObject.Type.Grass:
                    break;
                case MapGridObject.Type.Rock:
                    break;
                case MapGridObject.Type.Trees:
                    woodChange += 1;
                    break;
                case MapGridObject.Type.Mine:
                    graphiteChange += 1;
                    break;
                case MapGridObject.Type.Wind:
                    energyChange += 2;
                    break;
                case MapGridObject.Type.Solar:
                    energyChange += 1;
                    break;
                case MapGridObject.Type.Research:
                    researchChange += 1;
                    break;
            }
        }
    }
    public void UpdateValues()
    {
        energy += energyChange;
        research += researchChange;
        wood += woodChange;
        graphite += graphiteChange;
    }

    public void LimitValues()
    {
        if (energy > energyLimit) { energy = energyLimit; }
        if (research > researchLimit) { research = researchLimit; }
        if (wood > woodLimit) { wood = woodLimit; }
        if (graphite > graphiteLimit) { graphite = graphiteLimit; }
    }

    public void UpdateBarLimit()
    {
        energyBar.SetMaxValue(energyLimit);
        researchBar.SetMaxValue(researchLimit);
        woodBar.SetMaxValue(woodLimit);
        graphiteBar.SetMaxValue(graphiteLimit);
    }

    public void RemoveCost(int amount)
    {
        money -= amount;
    }
}
