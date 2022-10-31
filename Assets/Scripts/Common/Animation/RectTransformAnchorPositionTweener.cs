using UnityEngine;
using System.Collections;

public class RectTransformAnchorPositionTweener : Vector3Tweener{
    RectTransform rt;

    protected override void Awake(){
        base.Awake();
        rt = transform as RectTransform;
    }

    protected override void OnUpdate(object sender, System.EventArgs e){
        base.OnUpdate(sender, e);
        rt.anchoredPosition = currentValue;
    }
}

public static class RectTransformAnimationExtensions 
{
  public static Tweener AnchorTo (this RectTransform t, Vector3 position)
  {
    return AnchorTo (t, position, Tweener.DefaultDuration);
  }
  
  public static Tweener AnchorTo (this RectTransform t, Vector3 position, float duration)
  {
    return AnchorTo (t, position, duration, Tweener.DefaultEquation);
  }
  
  public static Tweener AnchorTo (this RectTransform t, Vector3 position, float duration, System.Func<float, float, float, float> equation)
  {
    RectTransformAnchorPositionTweener tweener = t.gameObject.AddComponent<RectTransformAnchorPositionTweener> ();
    tweener.startValue = t.anchoredPosition;
    tweener.endValue = position;
    tweener.easingControl.duration = duration;
    tweener.easingControl.equation = equation;
    tweener.easingControl.Play ();
    return tweener;
  }
}