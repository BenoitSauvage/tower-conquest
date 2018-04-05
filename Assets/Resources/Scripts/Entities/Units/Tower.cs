using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Unit {

    public override void Init() {
        base.Init();

        maxLife = GV.TOWER_MAX_LIFE;
        movingRange = GV.TOWER_MOVING_RANGE;
        coinCost = GV.TOWER_COINS_COST;

        attackMaxRange = GV.TOWER_MAX_ATTACK_RANGE;
        attackMinRange = GV.TOWER_MIN_ATTACK_RANGE;
        attackDamage = GV.TOWER_DAMAGE;

        life = maxLife;
        moves = movingRange;
    }

    private void TryToAttack (Vector2Int _cell) {
        if (GridManager.Instance.IsCellOccupied(_cell)) {
            Transform target = GridManager.Instance.GetUnitOnCell(_cell);

            if ((int)target.GetComponent<Unit>().GetPlayer() != (int)player)
                GridManager.Instance.HandleFight(target, attackDamage);
        }
    }

    public void AutoAttack() {
        float maxX = transform.position.x + (attackMaxRange * GV.GRID_CELL_SIZE);
        float minX = transform.position.x - (attackMaxRange * GV.GRID_CELL_SIZE);

        float maxZ = transform.position.z + (attackMaxRange * GV.GRID_CELL_SIZE);
        float minZ = transform.position.z - (attackMaxRange * GV.GRID_CELL_SIZE);

        float maxXY = GridManager.Instance.GetMaxXY();
        float minXY = -maxXY;

        Vector2Int center = new Vector2Int((int)transform.position.x, (int)transform.position.z);

        for (float x = minX; x <= maxX; x += GV.GRID_CELL_SIZE) {
            if (x >= minXY && x <= maxXY) {
                for (float z = minZ; z <= maxZ; z += GV.GRID_CELL_SIZE) {
                    if (z >= minXY && z <= maxXY) {
                        Vector2Int cellPos = new Vector2Int((int)x, (int)z);
                        if (Vector2Int.Distance(center, cellPos) >= (attackMinRange * GV.GRID_CELL_SIZE) && Vector2Int.Distance(center, cellPos) <= (attackMaxRange * GV.GRID_CELL_SIZE))
                            TryToAttack(cellPos);
                    }
                }
            }
        }
    }
}
