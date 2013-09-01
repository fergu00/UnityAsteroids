/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class AsteroidBig : AsteroidBase
{

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization before calling Start
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake () 
	{
		m_velocityMin = 0.2f;
		m_velocityMax = 0.3f;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// HitAsteroid: Overriden Function Called when we hit the Big asteroid 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	public override void HitAsteroid()
	{
		//Destroy The current Asteroid
		AsteroidsMgr.Instance.DeleteOneAsteroidFromList(gameObject);
		
		Vector3 posBigAsteroid = transform.localPosition;
		//Create 1st Normal Asteroid
		AsteroidsMgr.Instance.CreateRandomAsteroid(new Vector3(posBigAsteroid.x - 
			(GetComponent<tk2dSprite>().GetBounds().size.x /4),posBigAsteroid.y,0)); 
		//Create 2nd Normal Asteroid
		AsteroidsMgr.Instance.CreateRandomAsteroid(new Vector3(posBigAsteroid.x + 
			(GetComponent<tk2dSprite>().GetBounds().size.x /4),posBigAsteroid.y,0));
		
		//Play SOund
		SoundMgr.Instance.PlaySoundFX(SoundMgr.FXSounds.explote);
		
	}
}
