/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class ResizeWidthSprite : MonoBehaviour
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
	// Use this for initialization After Awake
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		// resize the current Sprite to fit with the  width of the screen
		localSprite = GetComponent<tk2dSprite>();
		Vector3 spriteScale = localSprite.scale;
		float spriteWidth =  localSprite.GetBounds().size.x;
		Camera cam = Camera.main;
		float worldWidth = (Screen.width * 2.0f * cam.orthographicSize) / Screen.height;
		//New Calculated spriteScale from camera resize * number of times should be multiplied
		spriteScale.x = spriteScale.x *(worldWidth / spriteWidth);
		localSprite.scale = spriteScale;
	}
}
