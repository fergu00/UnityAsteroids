/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class ITouch
{
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// PUBLIC UNITY PARAMETERS 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	public int fingerId;
	public TouchPhase phase;
	public Vector3 position;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public ITouch (int id, TouchPhase tphase, float posX, float posY)
	{
		fingerId = id;
		phase = tphase;
		position = new Vector3(posX, posY, 0.0f);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public ITouch (Touch t)
	{
		fingerId = t.fingerId;
		phase = t.phase;
		position = new Vector3(t.position.x, t.position.y, 0.0f);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public bool IsTouchOver (Camera camera, Collider area)
	{
		Vector3 worldPos = GetTouchWorldPos(camera);
		Vector3 dir = worldPos - camera.transform.position;
		Ray ray = new Ray(camera.transform.position, dir);
		RaycastHit info;
		
		Vector3 areaPos = area.transform.position;
		float posZ = areaPos.z;
		areaPos.z = 0.0f;
		area.transform.position = areaPos;
		bool collides = area.Raycast(ray, out info, Mathf.Abs(camera.transform.position.z * 2.0f));
		areaPos.z = posZ;
		area.transform.position = areaPos;
		
		return collides;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public Vector3 GetTouchWorldPos (Camera camera)
	{
		Vector3 worldPos = camera.ScreenToWorldPoint(position);
		worldPos.z = 0.0f;
		return worldPos;
	}
}

public class ITouches
{
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER STATIC VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER STATIC METHODS: 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// GetTouches: Touches handling from MultiTouch or Mouse
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public static ITouch[] GetTouches ()
	{
		//If We have more than One touchCount we have multitouch enabled
		if(Input.touchCount > 0)
		{
			ITouch[] touches = new ITouch[Input.touchCount];
			int iTouch = 0;
			foreach(Touch t in Input.touches)
			{
				touches[iTouch++] = new ITouch(t);
			}
			return touches;
		}
		else
		{
			//lets check if we have interacting with the mouse Buttons
			if(Input.GetMouseButtonDown(0))
			{
				ITouch[] touches = new ITouch[1];
				touches[0] = new ITouch(0, TouchPhase.Began, Input.mousePosition.x, Input.mousePosition.y);
				return touches;
			}
			else if(Input.GetMouseButtonUp(0))
			{
				ITouch[] touches = new ITouch[1];
				touches[0] = new ITouch(0, TouchPhase.Ended, Input.mousePosition.x, Input.mousePosition.y);
				return touches;
			}
			else if(Input.GetMouseButton(0))
			{
				ITouch[] touches = new ITouch[1];
				touches[0] = new ITouch(0, TouchPhase.Moved, Input.mousePosition.x, Input.mousePosition.y);
				return touches;
			}
		}
		return null;
	}
}
