/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class TopBar : MonoBehaviour 
{

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// PUBLIC UNITY PARAMETERS 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public tk2dSprite m_centralBar;
	public tk2dSprite m_borderBar;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		// resize background central bar to fit the screen width
		Vector3 barSize = m_centralBar.transform.localScale;
		float barWidth = m_centralBar.GetBounds().size.x;
		Camera cam = Camera.main;
		float worldWidth = (Screen.width * 2.0f * cam.orthographicSize) / Screen.height;
		float dBarW = worldWidth - (2.0f * m_borderBar.GetBounds().size.x);
		barSize.x = dBarW / barWidth;
		m_centralBar.scale = barSize;
	}		
}
