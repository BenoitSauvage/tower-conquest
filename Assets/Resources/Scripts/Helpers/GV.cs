using UnityEngine;
using System.Collections;

public class GV {

    // GENERAL
    public enum UNIT_TYPE { SOLDIER, CATAPULTE, TOWER };
    public static readonly int GRID_CELL_SIZE = 5;

    // GAME
    public static readonly float MAX_PLAYER = 2f;
    public static readonly float MAX_TURN_DURATION = 10f;
    public static readonly float PLAYER_SPAWN_AREA_SIZE = 2f;

    // UNITS
    public static readonly string UNIT_TAG_SOLDIER = "UnitSoldier";
    public static readonly string UNIT_TAG_CATAPULTE = "UnitCatapulte";
    public static readonly string UNIT_TAG_TOWER = "UnitTower";

    public static readonly string GHOST_UNIT_TAG = "GhostUnit";

    // SOLDIER
    public static readonly int SOLDIER_MOVING_RANGE = 2;
    public static readonly float SOLDIER_MAX_LIFE = 20f;

    // PLACEMENT / DEPLACEMENT
    public static readonly string CELL_PLACING_TAG = "PlacingCell";
    public static readonly string CELL_MOVING_TAG = "MovingCell";

}
