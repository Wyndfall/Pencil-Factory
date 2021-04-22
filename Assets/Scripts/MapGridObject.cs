using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGridObject
{

    public enum Type
    {
        Empty,
        Grass,
        Rock,
        Trees,
        Mine,
        Wind,
        Solar,
        Research,
    }

    private Grid<MapGridObject> grid;
    private int x;
    private int y;
    private Type type;

    public MapGridObject(Grid<MapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        type = Type.Empty;
    }
    public int GetX() => x;
    public int GetY() => y;

    public Type GetGridType()
    {
        return type;
    }

    public void SetGridType(Type type)
    {
        this.type = type;
        grid.TriggerGridObjectChanged(x,y);
    }
    public override string ToString()
    {
        return type.ToString();
    }
}
