using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GridSettings gridSettings;

    public bool debugOn = false;

    private MapGridObject.Type placeType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {
            if (debugOn) { gameManager.tilemap.SetTilemapType(worldPosition, placeType);
        }
        else
           {
                if (MouseWithinGrid(worldPosition))
                {
                    MapGridObject.Type clickPositionType = gameManager.tilemap.GetTilemapType(worldPosition);
                    if (clickPositionType != MapGridObject.Type.Rock && clickPositionType != placeType)
                    {
                        switch (placeType)
                        {
                            case MapGridObject.Type.Empty:
                                break;
                            case MapGridObject.Type.Grass:
                                if (gameManager.money >= gameManager.groundCost) { gameManager.money -= gameManager.groundCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                            case MapGridObject.Type.Rock:
                                break;
                            case MapGridObject.Type.Trees:
                                if (gameManager.money >= gameManager.treesCost) { gameManager.money -= gameManager.treesCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                            case MapGridObject.Type.Mine:
                                if (gameManager.money >= gameManager.mineCost) { gameManager.money -= gameManager.mineCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                            case MapGridObject.Type.Wind:
                                if (gameManager.money >= gameManager.windCost) { gameManager.money -= gameManager.windCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                            case MapGridObject.Type.Solar:
                                if (gameManager.money >= gameManager.solarCost) { gameManager.money -= gameManager.solarCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                            case MapGridObject.Type.Research:
                                if (gameManager.money >= gameManager.researchCost) { gameManager.money -= gameManager.researchCost; gameManager.tilemap.SetTilemapType(worldPosition, placeType); }
                                break;
                        }
                    }
                }
            }
        }
    }

    public void SetPlaceType(string type)
    {
        switch (type)
        {
            case "empty":
                placeType = MapGridObject.Type.Empty;
                break;
            case "grass":
                placeType = MapGridObject.Type.Grass;
                break;
            case "rock":
                placeType = MapGridObject.Type.Rock;
                break;
            case "trees":
                placeType = MapGridObject.Type.Trees;
                break;
            case "mine":
                placeType = MapGridObject.Type.Mine;
                break;
            case "wind":
                placeType = MapGridObject.Type.Wind;
                break;
            case "solar":
                placeType = MapGridObject.Type.Solar;
                break;
            case "research":
                placeType = MapGridObject.Type.Research;
                break;
        }
    }

    public static Vector3 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public bool MouseWithinGrid(Vector3 mouseWorldPosition)
    {
        if (mouseWorldPosition.x > gridSettings.offsetX && mouseWorldPosition.y > gridSettings.offsetY && mouseWorldPosition.x < (gridSettings.offsetX + (gridSettings.cellSize * gridSettings.gridWidth)) && mouseWorldPosition.y < (gridSettings.offsetY + (gridSettings.cellSize * gridSettings.gridHeight)))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void toggleDebug()
    {
        debugOn = !debugOn;
    }
}
