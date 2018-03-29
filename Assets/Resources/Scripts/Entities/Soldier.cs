﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour {

    public void DrawMovingCell () {
        float maxX = transform.position.x + (GV.SOLDIER_MOVING_RANGE * GV.GRID_CELL_SIZE);
        float minX = transform.position.x - (GV.SOLDIER_MOVING_RANGE * GV.GRID_CELL_SIZE);

        float maxZ = transform.position.z + (GV.SOLDIER_MOVING_RANGE * GV.GRID_CELL_SIZE);
        float minZ = transform.position.z - (GV.SOLDIER_MOVING_RANGE * GV.GRID_CELL_SIZE);

        float maxXY = GridManager.Instance.GetMaxXY();
        float minXY = -maxXY;

        Vector2Int center = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        for (float x = minX; x <= maxX; x += GV.GRID_CELL_SIZE) {
            if (x >= minXY && x <= maxXY) {
                for (float z = minZ; z <= maxZ; z += GV.GRID_CELL_SIZE) {
                    if (z >= minXY && z <= maxXY) {
                        Vector2Int cellPos = new Vector2Int((int)x, (int)z);
                        if (Vector2Int.Distance(center, cellPos) <= GV.SOLDIER_MOVING_RANGE * GV.GRID_CELL_SIZE)
                            GridManager.Instance.DrawMovingCell(x, z);
                    }
                }
            }
        }
    }
}