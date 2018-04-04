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
    private List<GameObject> attackCells = new List<GameObject>();

    private GameObject movingCellParent, placingCellParent, attackCellParent;
    private int maxXY, minXY;

    private bool hasUnitToRemove = false, hasUnitToKill = false;
    private float timePassed = 0f;
    private Vector2Int unitIndexToRemove;

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

        if (!attackCellParent) {
            attackCellParent = new GameObject();
            attackCellParent.name = "Attack Cell Parent";
        }
    }

    public void Update (float _dt) {
        if (hasUnitToKill) {
            UnitManager.Instance.KillUnit(grid[unitIndexToRemove]);
            hasUnitToKill = false;
        }

        if (hasUnitToRemove) {
            timePassed += _dt;

            if (timePassed >= GV.ANIMATION_DURATION) {
                timePassed = 0f;
                hasUnitToRemove = false;
                RemoveUnit(unitIndexToRemove);
            }
        }
    }

    public void NextTurn () {
        RemovePlacingCell();
        RemoveCellView();
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
                unit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Tower"));
                unit.GetComponent<Unit>().Init();
                unit.name = "Tower";
                break;
            case GV.UNIT_TYPE.CATAPULTE:
                unit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Catapulte"));
                unit.GetComponent<Unit>().Init();
                unit.name = "Catapulte";
                break;
            case GV.UNIT_TYPE.SOLDIER:
                unit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Soldier"));
                unit.GetComponent<Unit>().Init();
                unit.name = "Soldier";
                break;
        }

        if (unit) {
            unit.transform.position = ghostUnit.transform.position;
            unit.transform.SetParent(UnitManager.Instance.GetUnitsParent());

            foreach (Transform child in unit.transform) {
                if (child.CompareTag(GV.GENERIC_UNIT_TAG))
                    child.GetComponent<Renderer>().material = Material.Instantiate(
                        Resources.Load<Material>("Materials/Player_" + GameManager.Instance.GetCurrentPlayer()
                    ));
            }

            PlayerManager.Instance.UpdateCoins(unit.GetComponent<Unit>().GetCost());
            UIManager.Instance.UpdatePlayerInfos();
            UpdateGrid(unit.transform);
        }
       
        RemovePlacingCell();
    }

    public void ShowPlayerCells () {
        float player = GameManager.Instance.GetCurrentPlayer();
        int gridSize = GridManager.instance.GetMaxXY();

        float endSpanwArea = 0f;

        switch ((int)player) {
            case 1:
                endSpanwArea = -gridSize + (GV.PLAYER_SPAWN_AREA_SIZE * GV.GRID_CELL_SIZE);
                for (int z = -gridSize; z < endSpanwArea; z += GV.GRID_CELL_SIZE)
                    for (int x = gridSize; x >= -gridSize; x -= GV.GRID_CELL_SIZE) 
                        DrawPlacingCell(new Vector2Int(x, z));
                break;
            case 2:
                endSpanwArea = gridSize - (GV.PLAYER_SPAWN_AREA_SIZE * GV.GRID_CELL_SIZE);
                for (int z = gridSize; z > endSpanwArea; z -= GV.GRID_CELL_SIZE)
                    for (int x = gridSize; x >= -gridSize; x -= GV.GRID_CELL_SIZE)
                        DrawPlacingCell(new Vector2Int(x, z));
                break;
        }
    }

    public void CreateGhostUnit(GV.UNIT_TYPE _type) {
        switch (_type) {
            case GV.UNIT_TYPE.TOWER:
                ghostUnit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Tower"));
                break;
            case GV.UNIT_TYPE.CATAPULTE:
                ghostUnit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Catapulte"));
                break;
            case GV.UNIT_TYPE.SOLDIER:
                ghostUnit = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Units/Soldier"));
                break;
        }

        ghostUnit.name = "Placing Unit";
        ghostUnit.transform.SetParent(UnitManager.Instance.GetUnitsParent());
        ghostUnit.transform.tag = GV.GHOST_UNIT_TAG;
        ghostUnit.transform.position = new Vector3(-500, 0, -500);

        Material material = Material.Instantiate(
            Resources.Load<Material>("Materials/Player_" + GameManager.Instance.GetCurrentPlayer())
        );
        Color color = material.color;
        color.a = .2f;
        material.color = color;

        foreach (Transform child in ghostUnit.transform)
            if (child.CompareTag(GV.GENERIC_UNIT_TAG))
                child.GetComponent<Renderer>().material = material;

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

    public void RemoveUnit (Vector2Int _cell) {
        Transform unit = grid[_cell];
        grid[_cell] = null;

        GameObject.Destroy(unit.gameObject);
    }

    public void DrawPlacingCell (Vector2Int _cell) {
        if (!isCellOccupied(_cell)) {
            GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/PlacingCell"));
            cell.name = "Placing Cell";
            cell.transform.position = new Vector3(_cell.x, 0, _cell.y);
            cell.transform.SetParent(placingCellParent.transform);
            placingCells.Add(cell);
        }
    }

    public void DrawMovingCell (Vector2Int _cell) {
        if (!isCellOccupied(_cell)) {
            GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/MovingCell"));
            cell.name = "Moving Cell";
            cell.transform.position = new Vector3(_cell.x, 0, _cell.y);
            cell.transform.SetParent(movingCellParent.transform);
            movingCells.Add(cell);
        }
    }

    public void DrawAttackCell(Vector2Int _cell) {
        GameObject cell = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/AttackCell"));
        cell.name = "Attack Cell";
        cell.transform.position = new Vector3(_cell.x, 0, _cell.y);
        cell.transform.SetParent(attackCellParent.transform);
        attackCells.Add(cell);
    }

    public void HandleFight (Transform _target, float _damage) {
        Transform t = null;
        Vector2Int _cell = new Vector2Int((int)_target.position.x, (int)_target.position.z);
        grid.TryGetValue(_cell, out t);

        if (t != null) {
            Unit unit = t.GetComponent<Unit>();
            unit.TakeDamage(_damage);

            if (unit.GetLife() <= 0) {
                if (typeof(Castle) != unit.GetType()) {
                    hasUnitToKill = true;
                    hasUnitToRemove = true;
                    unitIndexToRemove = _cell;
                } else {
                    GameManager.Instance.EndGame(_target);
                }
            }
        }

        RemoveAttackView();
    }

    public void RemoveCellView() {
        if (movingCells.Count > 0) {
            foreach (GameObject cell in movingCells)
                GameObject.Destroy(cell);

            movingCells = new List<GameObject>();
        }
    }

    public void RemoveAttackView() {
        if (attackCells.Count > 0) {
            foreach (GameObject cell in attackCells)
                GameObject.Destroy(cell);

            attackCells = new List<GameObject>();
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