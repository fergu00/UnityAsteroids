/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour 
{
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// DATA DEFINITIONS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	public enum VAnchor {
		down,
		vcenter,
		up
	};
	
	public enum HAnchor {
		left,
		hcenter,
		right
	};
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	public Camera m_anchorCamera;
	public HAnchor m_horizontalAnchor;
	public VAnchor m_verticalAnchor;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake () 
	{
		AnchorElements();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// AnchorElements: Anchor Elements and sub elements into the exact horizontal and vertical position
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void AnchorElements ()
	{
		Camera anchorCamera = m_anchorCamera;
		if(anchorCamera == null)
		{
			anchorCamera = Camera.main;	
		}
		
		CameraResize cr = anchorCamera.gameObject.GetComponent<CameraResize>();
		if(cr != null)
		{
			cr.ResizeCamera();
		}
		
		Vector3 screenPos = new Vector3(0.0f, 0.0f, 0.0f);
		if(m_horizontalAnchor == HAnchor.right)
		{
			screenPos.x = Screen.width;	
		}
		else if(m_horizontalAnchor == HAnchor.hcenter)
		{
			screenPos.x = Screen.width / 2.0f;
		}
		
		if(m_verticalAnchor == VAnchor.up)
		{
			screenPos.y = Screen.height;	
		}
		else if(m_verticalAnchor == VAnchor.vcenter)
		{
			screenPos.y = Screen.height / 2.0f;
		}
		
		Vector3 camPos = anchorCamera.transform.position;
		float worldUnits = 2.0f * anchorCamera.orthographicSize;
		float screenUnits = Screen.height;
		
		Vector3 worldRefPos = screenPos;
		worldRefPos.x = camPos.x + (((screenPos.x - (Screen.width / 2.0f)) * worldUnits) / screenUnits);
		worldRefPos.y = camPos.y + (((screenPos.y - (Screen.height / 2.0f)) * worldUnits) / screenUnits);
		
		foreach (Transform child in transform) 
		{
			child.position = new Vector3(worldRefPos.x, worldRefPos.y, child.position.z);
        }
	}
	
}
