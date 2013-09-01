/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour 
{
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public float m_startAlpha = 0.0f;		// The transparency value to start fading from
	public float m_endAlpha = 1.0f;		// The transparency value to end fading at
	public float m_velocityFade = 2.0f;	 //velocity of the animation
	public GUITexture m_textureFade;
		
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private string[] m_levelToLoad ={"InitSplashes","MenuGame","AsteroidGame"};
	private float m_timerAlpha = 0.0f;			// The time passed since fading was enabled
	private bool m_fadingOn = false;		// Controls whether to fade or not
	private bool m_loadedLevel = false;
	private float m_endAlpha_private;
	private float m_startAlpha_private;
	private LevelsToLoad m_actualLevelLoaded = LevelsToLoad.k_splashes;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public enum LevelsToLoad
	{
		k_splashes,
		k_menu,
		k_game
		//...future next LevelsToLoad
	}

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// SINGLETON INSTANCE
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private static LevelLoader s_instance = null;
	public static LevelLoader Instance
	{
		get
		{
			return s_instance;
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake ()
	{
		// set singleton
		s_instance = this;
	}
	
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		//Get this gameobject available also in future scenes
		DontDestroyOnLoad(gameObject);
		
		//make the texture as the size of the screen
		m_textureFade.pixelInset = new Rect(0, 0, Screen.width+1, Screen.height );
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update () 
	{
		//Check if we have pressed Back Button
		BackButtonPressed();
		
		// Don't do anything if we are not fading
		if (!m_fadingOn)
			return;
		
		
		//Fade out !!!
		if(m_startAlpha_private > m_endAlpha_private)
		{
			m_timerAlpha -= Time.deltaTime * m_velocityFade;
			if(m_timerAlpha <= m_endAlpha_private)
			{
				m_timerAlpha = m_endAlpha_private;
				m_fadingOn = false;
				if(!m_loadedLevel)
					LoadLevel();
			}
		}
		//Fade in !!!
		else
		{
			m_timerAlpha += Time.deltaTime * m_velocityFade;
			if(m_timerAlpha > m_endAlpha_private)
			{
				m_timerAlpha = m_endAlpha_private;
				m_fadingOn = false;
				if(!m_loadedLevel)
					LoadLevel();
			}
		}
		
		m_textureFade.color = new Color(0,0,0,m_timerAlpha);
		
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// OnLevelWasLoaded: Called when a level is Load
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void OnLevelWasLoaded(int level) 
	{
    	//Start to fade in/out after loading the level
		m_fadingOn = true;
		
		//oppositte of it was selected
		float startAlpha =  m_startAlpha_private;
		float endAlpha = m_endAlpha_private;
		m_startAlpha_private = endAlpha;
		m_endAlpha_private = startAlpha;
		
		m_timerAlpha = m_startAlpha_private;
		if(m_startAlpha_private > m_endAlpha_private)
		{
			m_timerAlpha = 1.0f;
		}
		else
		{
			m_timerAlpha = 0.0f;
		}
    }
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// LoadLevel: Load level passed in the enum
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void LoadLevel(LevelsToLoad levelToLoad )
	{
		m_endAlpha_private = m_endAlpha;
		m_startAlpha_private = m_startAlpha;
		m_timerAlpha = m_startAlpha_private;
		if(m_startAlpha_private > m_endAlpha_private)
		{
			m_timerAlpha = 1.0f;
		}
		else
		{
			m_timerAlpha = 0.0f;
		}
		
		m_fadingOn = true;
		m_loadedLevel = false;
		
		//set actualLevelLoaded
		m_actualLevelLoaded = levelToLoad;
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// LoadLevel: Load level passed in the enum
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void LoadLevel()
	{
		m_loadedLevel = true;
		Application.LoadLevel(m_levelToLoad[(int)m_actualLevelLoaded]);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// BackButtonPressed: Back Button options depending in wich screen of the game we are
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private void BackButtonPressed()
	{
		if (Input.GetKeyDown(KeyCode.Escape)) 
		{ 
			switch(m_actualLevelLoaded)
			{
				case LevelsToLoad.k_splashes:
					Application.Quit();
				break;
				
				case LevelsToLoad.k_menu:
					Application.Quit();
				break;
				
				case LevelsToLoad.k_game:
					//Stop the Couroutine who creates the asteroids Animation randomly
					AsteroidsMgr.Instance.StopCreateAsteroids();
		
					//Delete all The asteroids in the animation
					AsteroidsMgr.Instance.DeleteAllAsteroidFromList();
				
					//Load Menu Scene
					LoadLevel(LevelsToLoad.k_menu);
				break;
				
			}
			
			
		}
	}
	
}
