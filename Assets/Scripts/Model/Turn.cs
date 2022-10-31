using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn
{
    public Unit unit;
    public bool moved;
    public bool acted;
    public bool item;
    public bool lockMove;
    Tile startTile;
    Directions startDir;

    public void Change(Unit current){
        unit = current;
        moved = false;
        acted = false;
        item = false;
        lockMove = false;
        startTile = unit.tile;
        startDir = unit.dir;
    }

    public void UndoMove(){
        moved = false;
        unit.tile = startTile;
        unit.dir = startDir;
        actor.Match();
    }
}
