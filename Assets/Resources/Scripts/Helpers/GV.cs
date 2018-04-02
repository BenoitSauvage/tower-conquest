using UnityEngine;
using System.Collections;

public class GV {

    // GENERAL
    public enum UNIT_TYPE { SOLDIER, CATAPULTE, TOWER };
    public enum ACTION_TYPE { MOVE, ATTACK };
    public static readonly int GRID_CELL_SIZE = 5;

    // GAME
    public static readonly float MAX_PLAYER = 2f;
    public static readonly float MAX_TURN_DURATION = 30f;
    public static readonly float PLAYER_SPAWN_AREA_SIZE = 2f;
    public static readonly float NEW_TURN_COINS = 10f;
    public static readonly float PLAYER_STARTING_COINS = 50f;

    // UNITS
    public static readonly string UNIT_TAG_SOLDIER = "UnitSoldier";
    public static readonly string UNIT_TAG_CATAPULTE = "UnitCatapulte";
    public static readonly string UNIT_TAG_TOWER = "UnitTower";

    public static readonly string GHOST_UNIT_TAG = "GhostUnit";

    // SOLDIER
    public static readonly int SOLDIER_MOVING_RANGE = 2;
    public static readonly int SOLDIER_MIN_ATTACK_RANGE = 1;
    public static readonly int SOLDIER_MAX_ATTACK_RANGE = 1;
    public static readonly float SOLDIER_MAX_LIFE = 20f;
    public static readonly float SOLDIER_COINS_COST = 20f;
    public static readonly float SOLDIER_DAMAGE = 10f;

    // CATAPULTE
    public static readonly int CATAPULTE_MOVING_RANGE = 1;
    public static readonly int CATAPULTE_MIN_ATTACK_RANGE = 3;
    public static readonly int CATAPULTE_MAX_ATTACK_RANGE = 5;
    public static readonly float CATAPULTE_MAX_LIFE = 50f;
    public static readonly float CATAPULTE_COINS_COST = 30f;
    public static readonly float CATAPULTE_DAMAGE = 25f;

    // PLACEMENT / DEPLACEMENT
    public static readonly string CELL_PLACING_TAG = "PlacingCell";
    public static readonly string CELL_MOVING_TAG = "MovingCell";
    public static readonly string CELL_ATTACK_TAG = "AttackCell";

}
