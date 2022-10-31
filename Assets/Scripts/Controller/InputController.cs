using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputController : MonoBehaviour 
{
	public static event EventHandler<InfoEventArgs<Point>> moveEvent;
	public static event EventHandler<InfoEventArgs<int>> fireEvent;
  public static event EventHandler<InfoEventArgs<Point>> clickEvent;

	Repeater _hor = new Repeater("Horizontal");
	Repeater _ver = new Repeater("Vertical");
	string[] _buttons = new string[] {"Fire1", "Fire2", "Fire3"};

	void Update () 
	{
		UpdateKeys();
    UpdateMouse();
	}

  void UpdateKeys(){
    int x = _hor.Update();
		int y = _ver.Update();
		if (x != 0 || y != 0)
		{
			if (moveEvent != null)
				moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
		}

		for (int i = 0; i < 3; ++i)
		{
			if (Input.GetButtonUp(_buttons[i]))
			{
				if (fireEvent != null)
					fireEvent(this, new InfoEventArgs<int>(i));
			}
		}
  }

  void UpdateMouse(){
    if(Input.GetMouseButtonDown(0)){
      Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
      RaycastHit hit;
      
      if( Physics.Raycast( ray, out hit, 100f ) )
      {
        if(hit.transform.gameObject.GetComponent<Tile>() || hit.transform.gameObject.GetComponent<Unit>())
          if (clickEvent != null)
            clickEvent(this, new InfoEventArgs<Point>(hit.transform.gameObject.GetComponent<Tile>().pos));
      }
    }  
  }
}

class Repeater
{
  const float threshold = .3f;
  const float threshold2 = .4f;
  const float rate = .20f;
  const float rate2 = 0.10f;
  float _next;
  float _barrier;
  bool _hold;
  bool _hold2;
  string _axis;
  public Repeater (string axisName)
  {
    _axis = axisName;
  }
  public int Update ()
  {
    int retValue = 0;
    int value = Mathf.RoundToInt( Input.GetAxisRaw(_axis) );
    if (value != 0)
    {
      if (Time.time > _next)
      {
        retValue = value;
        
        if(_hold2 && Time.time > _barrier){
          _next = Time.time + rate2;
        }
        else {
          _next = Time.time + (_hold ? rate : threshold);
        }
        if(!_hold){
          _hold2 = true;
          _barrier = _next + threshold2;
        }
        _hold = true;
      }
    }
    else
    {
      _hold = false;
      _next = 0;
      _barrier = 0;
      _hold2 = false;
    }
    
    return retValue;
  }
}