/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;
public class Global : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// STATIC MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//Global Variables to Use in the game
	public static float baseScreenWidth = 640.0f;
	public static float baseScreenHeight = 960.0f;
	public static float k_baseAspect = baseScreenWidth / baseScreenHeight;
	public static float k_baseWorldHeight = 2.0f;
	public static float k_baseWorldWidth = k_baseAspect * k_baseWorldHeight;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// STATIC MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// RandomXposition: Calculate the  X random position in the screen to Instantiate an Object
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public static float RandomXposition()
	{
		Random.seed++;
		int posRandomInPixels = Random.Range(0,Screen.width);
		float worldWidth = (Screen.width * 2.0f * Camera.main.orthographicSize) / Screen.height;
		float worldPosToCreateObject = worldWidth/2 -((posRandomInPixels * worldWidth)/Screen.width);
		return worldPosToCreateObject;
	}
}
