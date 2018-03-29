using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager {

    #region singleton
    private static GridManager instance;

    private GridManager() { }

    public static GridManager Instance {
        get {
            if (instance == null)
                instance = new GridManager();

            return instance;
        }
    }
    #endregion singleton

    private Dictionary<Vector2Int, Transform> grid = new Dictionary<Vector2Int, Transform>();
    private List<GameObject> movingCells = new List<GameObject>();

    private GameObject movingCellParent;

    private int maxXY, minXY;

    public void Init (int _gridSize, Transform _units) {
        // Grid generation
        maxXY = (_gridSize * GV.GRID_CELL_SIZE) / 2;
        minXY = -maxXY;

        for (int i = minXY; i <= maxXY; i += GV.GRID_CELL_SIZE) {
            for (int j = minXY; j <= maxXY; j += GV.GRID_CELL_SIZE)
                grid.Add(new Vector2Int(i, j), null);
        }

        foreach (Transform unit in _units) {
            UpdateGrid(unit);
        }

        if (!movingCellParent) {
            movingCellParent = new GameObject();
            movingCellParent.name = "Moving Cell Parent";
        }
    }

    public void UpdateGrid (Transform _unit) {
        Vector2Int pos = new Vector2Int((int)_unit.position.x, (int)_unit.position.z);
        grid[pos] = _unit;
    }

    public void UpdateGrid (Transform _unit, Vector2Int _oldPos) {
        grid[_oldPos] = null;

        Vector2Int pos = new Vector2Int((int)_unit.position.x, (int)_unit.position.z);
        grid[pos] = _unit;
    }

    public void DrawMovingCell(float x, float z) {
        if (!isCellOccupied(new Vector2Int((int)x, (int)z))) {
            GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MovingCell"));
            cell.name = "Moving Cell";
            cell.transform.position = new Vector3(x, 0, z);
            cell.transform.SetParent(movingCellParent.transform);
            movingCells.Add(cell);
        }
    }

    public void RemoveCellView() {
        if (movingCells.Count > 0) {
            foreach (GameObject cell in movingCells)
                GameObject.Destroy(cell);

            movingCells = new List<GameObject>();
        }
    }

    public bool isCellOccupied (Vector2Int _cell) {
        Transform t = null;
        grid.TryGetValue(_cell, out t);

        return t != null;
    }

    public int GetMaxXY () {
        return maxXY;
    }
}