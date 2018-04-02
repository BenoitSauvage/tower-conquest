using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    protected GV.UNIT_TYPE unitType;
    protected float maxLife, life, coinCost;

    protected float movingRange;
    protected float moves = 0f;
    protected float player;

    protected bool hasAttacked = false;

    public virtual void Init () {
        player = GameManager.Instance.GetCurrentPlayer();
    }

    public void DrawMovingCell() {
        float maxX = transform.position.x + (movingRange * GV.GRID_CELL_SIZE);
        float minX = transform.position.x - (movingRange * GV.GRID_CELL_SIZE);

        float maxZ = transform.position.z + (movingRange * GV.GRID_CELL_SIZE);
        float minZ = transform.position.z - (movingRange * GV.GRID_CELL_SIZE);

        float maxXY = GridManager.Instance.GetMaxXY();
        float minXY = -maxXY;

        Vector2Int center = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        for (float x = minX; x <= maxX; x += GV.GRID_CELL_SIZE) {
            if (x >= minXY && x <= maxXY) {
                for (float z = minZ; z <= maxZ; z += GV.GRID_CELL_SIZE) {
                    if (z >= minXY && z <= maxXY) {
                        Vector2Int cellPos = new Vector2Int((int)x, (int)z);
                        if (Vector2Int.Distance(center, cellPos) <= (movingRange - moves) * GV.GRID_CELL_SIZE)
                            GridManager.Instance.DrawMovingCell(x, z);
                    }
                }
            }
        }
    }

    public void Move (Transform _destination) {
        float distance = Mathf.Ceil(Vector3.Distance(transform.position, _destination.position) / GV.GRID_CELL_SIZE);
        moves += distance;

        transform.position = new Vector3(_destination.position.x, transform.position.y, _destination.position.z);
    }

    public void NextTurn () {
        moves = 0f;
        hasAttacked = false;
    }

    public float GetPlayer () {
        return player;
    }

    public float GetCost () {
        return coinCost;
    }
}
