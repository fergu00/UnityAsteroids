/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class AsteroidsRainMng : MonoBehaviour
{
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public tk2dTextMesh m_numPoints;
	public tk2dTextMesh m_numLives;
	public GUITexture m_textureLossLive;
	public GameObject m_gameOver;
	public tk2dTextMesh m_finalPoints;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//const int k_numMaxAsteroids = 10;
	const int k_numMaxLives = 5;
	const int k_velocityFlashTexture = 5;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private float m_TimeBetweenAsteroids = 3;
	private float m_TimeBetweenBigAsteroids = 8;
	private int m_numAsteroidsDestroyed = 0;
	private int m_numOfLivesLeft;
	private float m_timerAlpha = 0;
	private bool m_showHitFlashTexture = false;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// SINGLETON INSTANCE
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private static AsteroidsRainMng s_instance = null;
	public static AsteroidsRainMng Instance
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
	// Use this for initialization before Start
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake ()
	{
		// set singleton
		s_instance = this;
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization After Awake
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start ()
	{
		//Set The Num Maximum of Asteroids in The Splash Animation
		//AsteroidsMgr.Instance.NumMaximumAsteroids = k_numMaxAsteroids;
		
		//Set The initial num of lives
		m_numOfLivesLeft = k_numMaxLives;
		m_numLives.text = m_numOfLivesLeft.ToString();
		m_numLives.Commit();
		
		//Set The initial num of points
		m_numPoints.text = m_numAsteroidsDestroyed.ToString();
		m_numPoints.Commit();
		
		//set the event callback to call Start throw Asteroids when the time Start the Game
		TimeMgr.Instance.StartGame += StartThrowingAsteroids;
		TimeMgr.Instance.StopGame += FinishGame;
		
		//make the texture as the size of the screen
		m_textureLossLive.pixelInset = new Rect(0, 0, Screen.width+1, Screen.height );
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update () 
	{
		if(m_showHitFlashTexture)
		{
			m_timerAlpha += Time.deltaTime * k_velocityFlashTexture;
			if(m_timerAlpha > 1)
			{
				m_timerAlpha = 0;
				m_showHitFlashTexture = false;
			}
			m_textureLossLive.color = new Color(128,128,128,m_timerAlpha);
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// StartThrowingAsteroids: Start Creating and throwing Asterids
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void StartThrowingAsteroids () 
	{
		//Start Creating Asteroids fot making an animation
		AsteroidsMgr.Instance.StartThrowingAsteroids(m_TimeBetweenAsteroids, true);
		AsteroidsMgr.Instance.StartThrowingBigAsteroids(m_TimeBetweenBigAsteroids);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// FinishGame: Finish Game
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void FinishGame () 
	{
		//Destroy All Asteroids
		AsteroidsMgr.Instance.StopCreateAsteroids();
		AsteroidsMgr.Instance.DeleteAllAsteroidFromList();
		
		//Show GameOver with the final Points
		m_gameOver.SetActive(true);
		m_finalPoints.text = m_numAsteroidsDestroyed.ToString();
		m_finalPoints.Commit();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// AddOneAsteroidDestroyedCounter: Add one asteroid Destroyed to the Counter
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void AddOneAsteroidDestroyedCounter () 
	{
		m_numAsteroidsDestroyed++;
		m_numPoints.text = m_numAsteroidsDestroyed.ToString();
		m_numPoints.Commit();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// LossALive: Loss A live and check if we have finished all the lives so Finish Game
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void LossALive () 
	{
		m_numOfLivesLeft--;
		m_numLives .text = m_numOfLivesLeft.ToString();
		m_numLives.Commit();
		
		//Show Flash red texture
		m_showHitFlashTexture = true;
		
		// Vibrate the phone!
		 Handheld.Vibrate();
		
		if(m_numOfLivesLeft == 0)
		{
			//Debug.Log("Finish Game");
			TimeMgr.Instance.FinishGame();
			
			//Show GameOver with the final Points
			m_gameOver.SetActive(true);
			m_finalPoints.text = m_numAsteroidsDestroyed.ToString();
			m_finalPoints.Commit();
		}
		
		//Play FX of losing a life
		SoundMgr.Instance.PlaySoundFX(SoundMgr.FXSounds.lossLive);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// RestartGame: Restart The game To play a new Game
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void RestartGame() 
	{
		//Load Main Game Level Again
		LevelLoader.Instance.LoadLevel(LevelLoader.LevelsToLoad.k_game);
	}
	
}
