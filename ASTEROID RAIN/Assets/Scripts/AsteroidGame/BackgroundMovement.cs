/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class BackgroundMovement : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public GameObject m_background;
	public GameObject m_stars;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CONSTANT  VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public const string k_nameStarPrefab = "Stars";
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private float m_scaleY;
	private float m_initialScaleY;
	private float m_movementPerSecond;
	private bool m_playAnimation = false;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		//set the event callback when we finish the Game before finishing Time
		TimeMgr.Instance.StopGame += FinishGame;
		
		m_initialScaleY = m_background.transform.localScale.y;
		
		//Calculate the number of scaled size * (2 units world) minus the size of the fist two units to leave the
		//background good positioned at the bottom
		m_scaleY = (m_background.transform.localScale.y * 2) - 2;
		m_movementPerSecond = m_scaleY / TimeMgr.m_secondsToPlay;
		
		//Reset Position of The background if the size of the camera is more than 1
		if(Camera.mainCamera.orthographicSize > 1)
		{
			m_background.transform.localScale = new Vector3(m_background.transform.localScale.x,
				m_background.transform.localScale.y + (Camera.mainCamera.orthographicSize - 1),
				m_background.transform.localScale.z);
		}
		
		
		//Check if we have to Start the animation or not
		CheckStartAnimation();
		
		//Instantiate Stars in the background
		InstantiateStars();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		if(m_playAnimation)
		{
			transform.Translate(Vector3.up * m_movementPerSecond * Time.deltaTime);
			if(transform.localPosition.y >= m_initialScaleY)
			{
				//Finished Game
				transform.localPosition = new Vector3(transform.localPosition.x, m_initialScaleY,transform.localPosition.z);
				m_playAnimation = false;
			}
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// PlayAnimation: Start playing the animation
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void PlayAnimation()
	{
		m_playAnimation = true;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CheckStartAnimation : Check if the animation has to start from the beginning or not
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void CheckStartAnimation()
	{
		if(TimeMgr.Instance == null)	
		{
			m_playAnimation = true;
		}else
		{
			//set the event callback to call PlayAnimation when the time Start the Game
			TimeMgr.Instance.StartGame += PlayAnimation;
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// InstantiateStars : Instantiate the stars to add to the make the background a feeling of movement
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void InstantiateStars()
	{
		float posToAddStar = 0;
		while(posToAddStar > -(m_background.transform.localScale.y/3)*2)
		{
			float posX = Global.RandomXposition();
			GameObject stars = (GameObject)Instantiate(Resources.Load(k_nameStarPrefab));
			stars.transform.parent = m_stars.transform;
			stars.transform.localPosition = new Vector3(posX,posToAddStar,stars.transform.localPosition.z);
			posToAddStar -= 0.2f;
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// FinishGame : Finished game so stop the Movement
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void FinishGame()
	{
		m_playAnimation = false;
	}
}
