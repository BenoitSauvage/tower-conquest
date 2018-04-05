using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    [HideInInspector]
    public float attackMinRange;
    [HideInInspector]
    public float attackMaxRange;

    protected GV.UNIT_TYPE unitType;
    protected float maxLife, life, coinCost;

    protected float movingRange;
    protected float attackDamage;
    protected float moves = 0f;
    protected float player;

    protected bool hasAttacked = false;
    protected Transform lifeBar;

    private float lifeBarScale;

    private void Start() {
        foreach (Transform child in transform) {
            if (child.CompareTag(GV.GENERIC_UNIT_TAG))
                child.GetComponent<Renderer>().material = Material.Instantiate(
                    Resources.Load<Material>("Materials/Player_" + GameManager.Instance.GetCurrentPlayer())
                );
            
            if (child.CompareTag(GV.LIFE_BAR_TAG)) {
                lifeBar = child;
                lifeBarScale = lifeBar.localScale.x;
            }
        }
    }

	public virtual void Init () {
        hasAttacked = true;
        player = GameManager.Instance.GetCurrentPlayer();

        if (lifeBar)
            lifeBarScale = lifeBar.localScale.x;
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
                            GridManager.Instance.DrawMovingCell(cellPos);
                    }
                }
            }
        }
    }

    public void DrawAttackCell() {
        if (hasAttacked)
            return;
        
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
                            GridManager.Instance.DrawAttackCell(cellPos);
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

    public void Attack (Transform _target) {
        float player = GameManager.Instance.GetCurrentPlayer();
        GameObject go = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/Particles/Player" + player + "attack"));
        Vector3 pos = _target.position;
        pos.y = 2;
        go.transform.position = pos;

        GridManager.Instance.HandleFight(_target, attackDamage);
        hasAttacked = true;
    }

    public float GetLife() {
        return life;
    }

    public void TakeDamage (float _damage) {
        life -= _damage;

        Vector3 lifeScale = lifeBar.localScale;

        lifeScale.x = (life / maxLife) * lifeBarScale;
        lifeBar.localScale = lifeScale;

        if (GetType() == typeof(Soldier))
            GetComponent<SoldierBehaviour>().UpdateLife(life, maxLife);
    }

    public void NextTurn () {
        moves = 0f;
        hasAttacked = false;

        if (GetType() == typeof(Tower))
            GetComponent<Tower>().AutoAttack();
    }

    public float GetPlayer () {
        return player;
    }

    public float GetCost () {
        return coinCost;
    }
}
