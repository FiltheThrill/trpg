using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : State
{
    protected BattleController owner;
    public CameraRig cameraRig {get{return owner.cameraRig;}}
    public Board board {get{return owner.board;}}
    public LevelData levelData {get{return owner.levelData;}}
    public Transform tsi {get{return owner.tsi;}}
    public Point pos {get{return owner.pos;} set{owner.pos = value;}}

    public AbilityMenuPanelController abilityMenuPanelController { get { return owner.abilityMenuPanelController; }}
    public Turn turn { get { return owner.turn; }}
    public List<Unit> units { get { return owner.units; }}

    protected virtual void Awake(){
        owner = GetComponent<BattleController>();
    }

    protected override void AddListeners(){
        InputController.moveEvent += OnMove;
        InputController.fireEvent += OnFire;
        InputController.clickEvent += OnClick;
    }

    protected override void RemoveListeners(){
        InputController.moveEvent -= OnMove;
        InputController.fireEvent -= OnFire;
        InputController.clickEvent -= OnClick;
    }
    
    protected virtual void OnMove(object sender, InfoEventArgs<Point> e){

    }

    protected virtual void OnClick(object sender, InfoEventArgs<Point> e){

    }

    protected virtual void OnFire(object sender, InfoEventArgs<int> e){

    }

    protected virtual void SelectTile(Point p){
        if(pos == p || !board.tiles.ContainsKey(p))
            return;
        pos = p;
        tsi.localPosition = board.tiles[p].center;
    }
}
