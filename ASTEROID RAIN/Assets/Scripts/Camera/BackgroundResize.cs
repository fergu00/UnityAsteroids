/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class BackgroundResize : MonoBehaviour
{

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	tk2dSprite localSprite;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization Before Start
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		Camera cam = Camera.main;
		//Getting the aspect ratio - if is smaller than our 960x640 calculate how much has to be scale to fit with the actual screen
		localSprite = GetComponent<tk2dSprite>();
		float targetWidth = Global.k_baseAspect * Screen.height;
		if(Screen.width < targetWidth)
		{
			//calculate how many times we have to scale our image to fit to the aspect ratio of the camera
			float finalScale = cam.orthographicSize*2 / localSprite.GetBounds().size.y;
			localSprite.scale = new Vector3(finalScale,finalScale,localSprite.transform.localScale.z);
		}	
	}
	
	
}
