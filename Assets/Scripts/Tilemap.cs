using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap
{
    private Grid<MapGridObject> grid;

    public Tilemap(int width, int height, float cellSize, Vector3 originPosition)
    {
        grid = new Grid<MapGridObject>(width, height, cellSize, originPosition, (Grid<MapGridObject> grid, int x, int y) => new MapGridObject(grid, x, y));
    }

    public void SetTilemapType(Vector3 worldPosition, MapGridObject.Type tilemapType)
    {
        MapGridObject mapGridObject = grid.GetGridObject(worldPosition);
        if (mapGridObject != null)
        {
            mapGridObject.SetGridType(tilemapType);
        }
    }

    public MapGridObject.Type GetTilemapType(Vector3 worldPosition)
    {
        MapGridObject mapGridObject = grid.GetGridObject(worldPosition);
        return mapGridObject.GetGridType();
    }

    public List<MapGridObject> GetMapGridObjectList()
    {
        List<MapGridObject> gridObjectList = new List<MapGridObject>();
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                gridObjectList.Add(grid.GetGridObject(x, y));
                //Debug.Log("Got " + grid.GetGridObject(x, y).ToString());
            }
        }
        return gridObjectList;
    }

    public Grid<MapGridObject> GetGrid()
    {
        return grid;
    }
}
