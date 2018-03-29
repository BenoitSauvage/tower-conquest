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

    private GameObject ghostUnit;
    private GV.UNIT_TYPE ghostUnitType;

    private Dictionary<Vector2Int, Transform> grid = new Dictionary<Vector2Int, Transform>();
    private List<GameObject> movingCells = new List<GameObject>();
    private List<GameObject> placingCells = new List<GameObject>();

    private GameObject movingCellParent, placingCellParent;
    private int maxXY, minXY;

    public void Init (int _gridSize, Transform _units) {
        // -- GRID GENERATION
        maxXY = (_gridSize * GV.GRID_CELL_SIZE) / 2;
        minXY = -maxXY;

        for (int i = minXY; i <= maxXY; i += GV.GRID_CELL_SIZE) {
            for (int j = minXY; j <= maxXY; j += GV.GRID_CELL_SIZE)
                grid.Add(new Vector2Int(i, j), null);
        }

        foreach (Transform unit in _units) {
            UpdateGrid(unit);
        }
        // -- END GRID GENERATION

        if (!movingCellParent) {
            movingCellParent = new GameObject();
            movingCellParent.name = "Moving Cell Parent";
        }

        if (!placingCellParent) {
            placingCellParent = new GameObject();
            placingCellParent.name = "Placing Cell Parent";
        }
    }

    public void ShowUnitGhost(Vector3 _position) {
        int x = (int)Mathf.Round(_position.x / GV.GRID_CELL_SIZE) * GV.GRID_CELL_SIZE;
        int z = (int)Mathf.Round(_position.z / GV.GRID_CELL_SIZE) * GV.GRID_CELL_SIZE;

        ghostUnit.transform.position = new Vector3(x, ghostUnit.transform.position.y, z);
    }

    public void PlaceUnit() {
        GameObject unit = null;

        switch (ghostUnitType) {
            case GV.UNIT_TYPE.TOWER:
            case GV.UNIT_TYPE.CATAPULTE:
            case GV.UNIT_TYPE.SOLDIER:
                unit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Soldier"));
                unit.name = "Soldier";
                break;
        }

        if (unit) {
            unit.transform.position = ghostUnit.transform.position;
            unit.transform.SetParent(UnitManager.Instance.GetUnitsParent());

            MeshRenderer renderer = unit.GetComponent<MeshRenderer>();
            Color color = renderer.material.color;
            color.a = 1f;
            renderer.material.color = color;

            UpdateGrid(unit.transform);
        }
       
        RemovePlacingCell();
    }

    public void ShowPlayerCells () {
        if (ghostUnit) {
            MeshRenderer renderer = ghostUnit.GetComponent<MeshRenderer>();
            Color color = renderer.material.color;
            color.a = .2f;
            renderer.material.color = color;
            ghostUnit.name = "Placing Unit";
            ghostUnit.transform.SetParent(UnitManager.Instance.GetUnitsParent());
        }

        float player = GameManager.Instance.GetCurrentPlayer();
        int gridSize = GridManager.instance.GetMaxXY();

        float endSpanwArea = 0f;

        switch ((int)player) {
            case 1:
                endSpanwArea = -gridSize + (GV.PLAYER_SPAWN_AREA_SIZE * GV.GRID_CELL_SIZE);
                for (int z = -gridSize; z < endSpanwArea; z += GV.GRID_CELL_SIZE)
                    for (int x = gridSize; x >= -gridSize; x -= GV.GRID_CELL_SIZE) 
                        DrawPlacingCell(x, z);
                break;
            case 2:
                endSpanwArea = gridSize - (GV.PLAYER_SPAWN_AREA_SIZE * GV.GRID_CELL_SIZE);
                for (int z = gridSize; z > endSpanwArea; z -= GV.GRID_CELL_SIZE)
                    for (int x = gridSize; x >= -gridSize; x -= GV.GRID_CELL_SIZE)
                        DrawPlacingCell(x, z);
                break;
        }
    }

    public void CreateGhostUnit(GV.UNIT_TYPE _type) {
        switch (_type) {
            case GV.UNIT_TYPE.TOWER:
            case GV.UNIT_TYPE.CATAPULTE:
            case GV.UNIT_TYPE.SOLDIER:
                ghostUnit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Soldier"));
                break;
        }

        ghostUnit.transform.tag = GV.GHOST_UNIT_TAG;
        ghostUnitType = _type;
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

    public void DrawPlacingCell (float x, float z) {
        if (!isCellOccupied(new Vector2Int((int)x, (int)z))) {
            GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PlacingCell"));
            cell.name = "Placing Cell";
            cell.transform.position = new Vector3(x, 0, z);
            cell.transform.SetParent(placingCellParent.transform);
            placingCells.Add(cell);
        }
    }

    public void DrawMovingCell (float x, float z) {
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

    public void RemovePlacingCell() {
        if (ghostUnit) {
            GameObject.Destroy(ghostUnit);
            ghostUnit = null;
        }

        if (placingCells.Count > 0) {
            foreach (GameObject cell in placingCells)
                GameObject.Destroy(cell);

            placingCells = new List<GameObject>();
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