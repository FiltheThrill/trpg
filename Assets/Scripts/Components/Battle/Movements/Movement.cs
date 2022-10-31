using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    public int reach;
    public int jump;
    protected Unit unit;
    protected Transform jumper;

    protected virtual void Awake(){
        unit = GetComponent<Unit>();
        jumper = transform.Find("Jumper");
    }

    public virtual List<Tile> GetTilesInReach(Board board){
        List<Tile> ret = board.Search(unit.tile, ExpandSearch);
        Filter(ret);
        return ret;
    }

    protected virtual bool ExpandSearch(Tile from, Tile to){
        return (from.distance + 1) <= reach;
    }

    protected void Filter(List<Tile> tiles){
        for(int i = tiles.Count - 1; i >= 0; --i){
            if(tiles[i].content != null){
                tiles.RemoveAt(i);
            }
        }
    }

    public abstract IEnumerator Traverse(Tile tile);

    protected virtual IEnumerator Turn(Directions dir){
        TransformLocalEulerTweener t = (TransformLocalEulerTweener)transform.RotateToLocal(dir.ToEuler(), 0.25f, EasingEquations.EaseInOutQuad);

        if(Mathf.Approximately(t.startValue.y, 0f) && Mathf.Approximately(t.endValue.y, 270f))
            t.startValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        else if (Mathf.Approximately(t.startValue.y, 270) && Mathf.Approximately(t.endValue.y, 0))
            t.endValue = new Vector3(t.startValue.x, 360f, t.startValue.z);
        unit.dir = dir;
        
        while (t != null)
            yield return null;
    }
}
