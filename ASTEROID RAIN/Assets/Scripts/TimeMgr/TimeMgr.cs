/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class TimeMgr : MonoBehaviour 
{

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// PUBLIC UNITY PARAMETERS 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public tk2dSprite m_321Sprite;
	public tk2dTextMesh m_TimeLeftText;
	public tk2dSprite m_watchHand;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public const int m_secondsToPlay = 60;
	public const int m_degreesInSphere = 360;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private bool m_start = false;
	private float m_timePlaying = 0;
	private float m_degreesPerSecond = 0;
	private string[] m_321Animation=  {"3","2","1"};
	private enum enum321Animation
	{
		num3,
		num2,
		num1
	}
	
	
	//events delegates
	public event StartTimeGame StartGame;
	public event StopTimeGame StopGame;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// SINGLETON INSTANCE
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private static TimeMgr s_instance = null;
	public static TimeMgr Instance
	{
		get
		{
			return s_instance;
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public delegate void StartTimeGame();
	public delegate void StopTimeGame();
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
		//start 321 Animation
		StartCoroutine("Start321Animation");
		
		//Change Num MAX of asteroids to make it more difficult over time!
		StartCoroutine("ChangeNumAsteroids");
		
		//Initialize Timer Text in the TopBar with our m_secondsToPlay of playing
		m_TimeLeftText.text = m_secondsToPlay.ToString();
		m_TimeLeftText.Commit();
		
		//Initialize num of Degrees per second
		m_degreesPerSecond = m_degreesInSphere/m_secondsToPlay;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		if(m_start)
		{
			m_timePlaying += Time.deltaTime;
			
			m_TimeLeftText.text = ((int)m_secondsToPlay - (int)m_timePlaying).ToString();
			m_TimeLeftText.Commit();
			
			//rotate the watch hand to show the elapsed time
			m_watchHand.transform.Rotate(0,0, m_degreesPerSecond * Time.deltaTime, Space.Self);
			
			//Finish game if we have have played all the time
			if(m_timePlaying >= m_secondsToPlay)
			{
				//Debug.Log("FINISH GAME");
				m_start = false;
				
				//Call The StopGame Event
				StopGame();
				
			}
			
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Start321Animation: Ienumerator who Starts 321 ANIMATION and when is it finished start the game
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public IEnumerator Start321Animation ()
	{
		yield return new WaitForSeconds(1.0f);
		m_321Sprite.SetSprite(m_321Animation[(int)enum321Animation.num2]);
		yield return new WaitForSeconds(1.0f);
		m_321Sprite.SetSprite(m_321Animation[(int)enum321Animation.num1]);
		yield return new WaitForSeconds(1.0f);
		Destroy(m_321Sprite);
		m_start = true;
		//Call The StartGame Event
		if (StartGame != null)
			StartGame();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// FinishGame: Finish the game as we have lost all our lives
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void FinishGame ()
	{
		m_start = false;
		//Call The StopGame Event
		if (StopGame != null)
		StopGame();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// ChangeNumAsteroids: Change the number of asteroids during game to make it more difficult over time!
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public IEnumerator ChangeNumAsteroids ()
	{
		int numSecondsToChange = m_secondsToPlay/4;
		AsteroidsMgr.Instance.NumMaximumAsteroids = 5;
		yield return new WaitForSeconds(numSecondsToChange);
		AsteroidsMgr.Instance.NumMaximumAsteroids = 8;
		yield return new WaitForSeconds(numSecondsToChange);
		AsteroidsMgr.Instance.NumMaximumAsteroids = 11;
		yield return new WaitForSeconds(numSecondsToChange);
		AsteroidsMgr.Instance.NumMaximumAsteroids = 15;
	}
	

}
