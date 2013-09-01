/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class AsteroidBase : MonoBehaviour {

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------	
	protected float m_velocityMin;
	protected float m_velocityMax;
	
	private float m_velocity;
	private tk2dSprite m_sprite;
	private bool m_animationPlaying = true;
	private ITouch[] m_allTouches = null;
	private MeshCollider m_spriteCollider;
	private bool m_canBeDestroyed = true;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start ()
	{
		m_sprite = GetComponent<tk2dSprite>();
		m_velocity = Random.Range(m_velocityMin,m_velocityMax);
		
		//Asign the collider
		m_spriteCollider = GetComponent<MeshCollider>();
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		if(m_animationPlaying)
		{
			//Falling down Movement
			FallDownAsteroids();
			
			//Check Touch
			UpdateTouches();
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CanBeDestroyed: Get/Set if the Asteroid can Be Destroyed
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public bool CanBeDestroyed
	{
		get { return m_canBeDestroyed; }
		set
		{
			if (m_canBeDestroyed != value)
			{
				m_canBeDestroyed = value;
			}
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// AnimationIsPlaying: Get/Set Variable to controll if the animation has to play/isPlaying
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public bool AnimationIsPlaying
	{
		get { return m_animationPlaying; }
		set
		{
			if (m_animationPlaying != value)
			{
				m_animationPlaying = value;
			}
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// FallDownAsteroids: Move the asteroids down with different speed
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void FallDownAsteroids()
	{
		//Move Down the Asteroids with the velocity of each class
		transform.Translate(Vector3.down * Time.deltaTime * m_velocity);
		// Loss a live and Destroy the Asteroid if the user has not destroyed before arriving to London :)
		float posToDestroy = Camera.mainCamera.transform.localPosition.y - Camera.mainCamera.orthographicSize - m_sprite.GetBounds().size.y;
		if(transform.localPosition.y < posToDestroy)
		{
			//We have missed to destroy the Asteroid so we have loss a live!
			AsteroidsMgr.Instance.DeleteOneAsteroidFromList(gameObject);
			
			//Create a new Asteroid to make it Funnier
			AsteroidsMgr.Instance.CreateRandomAsteroid();
			
			//loss a live
			if(AsteroidsRainMng.Instance)
				AsteroidsRainMng.Instance.LossALive();
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// HitAsteroid: Virtual Function Called when we hit the asteroid and in the base we are going to destroy it 
	//and create a new one. Can be Override if the derived class wants another behaviour
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public virtual void HitAsteroid()
	{
		//Add One To the counter of Destroyed Asteoroids
		AsteroidsRainMng.Instance.AddOneAsteroidDestroyedCounter();
		
		//Destroy The current Asteroid
		AsteroidsMgr.Instance.DeleteOneAsteroidFromList(gameObject);
		
		//Create a new Asteroid to make it Funnier
		AsteroidsMgr.Instance.CreateRandomAsteroid();
		
		//Play SOund
		SoundMgr.Instance.PlaySoundFX(SoundMgr.FXSounds.explote);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UpdateTouches: Check from all the Touches in the screen if we have hit any Asteroid and called HitAsteroid() function
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void UpdateTouches()
	{
		if(m_canBeDestroyed)
		{
			m_allTouches = ITouches.GetTouches();
			if( m_allTouches != null)
			{
				if(m_allTouches.Length >0)
				{
					foreach (ITouch touch in m_allTouches)
					{
						if(touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
						{
							if(touch.IsTouchOver(Camera.main, m_spriteCollider))
							{
								//Debug.Log("HIT ASTEROID!!!!!!");
								HitAsteroid();
							}
						}
					}
				}
			}
		}
	}
}
