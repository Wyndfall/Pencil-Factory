using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridSettings gridSettings;
    [SerializeField] private GridPrefabVisual gridPrefabVisual;

    private Tilemap tilemap;
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

    public int energyBar;
    public int researchBar;
    public int woodBar;
    public int graphiteBar;

    [Header("Large Resources")]

    public int money;
    public TextMeshProUGUI moneyCounter;


    private void Start()
    {
        //create the tilemap
        tilemap = new Tilemap(gridSettings.gridWidth, gridSettings.gridHeight, gridSettings.cellSize, new Vector3(gridSettings.offsetX, gridSettings.offsetY));
        //setup the tilemap
        gridPrefabVisual.Setup(tilemap.GetGrid());
    }

    private void Update()
    {
        //run any code inside this loop every game tick
        if (Time.time > nextActionTime)
        {
            nextActionTime += gridSettings.tickSpeed;

            RunGridLoop();
        }

        //Input Loops
            Vector3 worldPosition = GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {
            tilemap.SetTilemapType(worldPosition, MapGridObject.Type.Rock);
        }
        if (Input.GetMouseButtonDown(1))
        {
            tilemap.SetTilemapType(worldPosition, MapGridObject.Type.Empty);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
    }

    public void RunGridLoop()
    {
        List<MapGridObject> mapGridObjectList = tilemap.GetMapGridObjectList();
        foreach (MapGridObject mapGridObject in mapGridObjectList)
        {
            switch (mapGridObject.GetGridType())
            {
                case MapGridObject.Type.Empty:
                    //Debug.Log("Empty");
                    break;
                case MapGridObject.Type.Grass:
                    //Debug.Log("Grass");
                    break;
                case MapGridObject.Type.Rock:
                    //Debug.Log("Rock");
                    break;
            }
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
