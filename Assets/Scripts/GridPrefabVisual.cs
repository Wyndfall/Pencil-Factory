using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPrefabVisual : MonoBehaviour
{
    public static GridPrefabVisual Instance { get; private set; }

    [SerializeField] private GridSettings gridSettings;
    [SerializeField] public Transform EmptyPrefab;
    [SerializeField] private Transform GrassPrefab;
    [SerializeField] private Transform RockPrefab;

    private Grid<MapGridObject> grid;
    private bool updateVisual;


    private void Awake()
    {
        Instance = this;
    }

    public void Setup(Grid<MapGridObject> grid)
    {
        this.grid = grid;
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector3 gridPosition = new Vector3(x + gridSettings.offsetX, y + gridSettings.offsetY) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f;
                Transform visualNode = CreateVisualNode(gridPosition, GrassPrefab);
            }
        }

        grid.OnGridObjectChanged += Grid_OnGridObjectChanged;
    }

    private void Update()
    {
        if (updateVisual)
        {
            updateVisual = false;
        }
    }
    private void Grid_OnGridObjectChanged(object sender, Grid<MapGridObject>.OnGridObjectChangedEventArgs e)
    {
        MapGridObject gridObject = grid.GetGridObject(e.x, e.y);
        Vector3 gridPosition = new Vector3(e.x + gridSettings.offsetX, e.y + gridSettings.offsetY) * grid.GetCellSize() + Vector3.one * grid.GetCellSize() * .5f;
        ReCreateNodeChanged(gridPosition, gridObject);
    }

    private void ReCreateNodeChanged(Vector3 position, MapGridObject gridObject)
    {
        switch (gridObject.GetGridType())
        {
            case MapGridObject.Type.Empty:
                ReCreateVisualNode(position, EmptyPrefab);
                break;
            case MapGridObject.Type.Grass:
                ReCreateVisualNode(position, GrassPrefab);
                break;
            case MapGridObject.Type.Rock:
                ReCreateVisualNode(position, RockPrefab);
                break;
        }
    }

    private Transform CreateVisualNode(Vector3 position, Transform transform)
    {
        Transform visualNodeTransform = Instantiate(transform, position, Quaternion.identity);
        return visualNodeTransform;
    }
    private Transform ReCreateVisualNode(Vector3 position, Transform transform)
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, 0.01f);
        foreach (var hitCollider in hitColliders)
        {
            Destroy(hitCollider.gameObject);
        }
        Transform visualNodeTransform = Instantiate(transform, position, Quaternion.identity);
        return visualNodeTransform;
    }
}
