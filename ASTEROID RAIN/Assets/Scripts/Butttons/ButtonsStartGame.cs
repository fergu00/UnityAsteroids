/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class ButtonsStartGame : MonoBehaviour {

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public tk2dTextMesh m_textButton;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	const float k_velFlahsingText = 1.5f;
	const float k_velFlahsingButton = 0.5f;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private tk2dSprite m_button;
	private bool m_start = true;
	private bool m_flash = true;
	private float m_timeAlpha = 0;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER METHODS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		m_button = GetComponent<tk2dSprite>();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		FlashingButtonText();
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  FlashingButtonText: Generate Alpha animation to the start button and text
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void FlashingButtonText()
	{
		//Initialize first flahing: ButtonBackgound and Text
		if(m_start)
		{
			m_timeAlpha += Time.deltaTime * k_velFlahsingButton;
			m_button.color = new Color(1,1,1,m_timeAlpha);
			m_textButton.color = new Color(m_textButton.color.r,m_textButton.color.g,m_textButton.color.b,m_timeAlpha);
			m_textButton.Commit();
			if(m_timeAlpha > 1.0f)
				m_start = false;
		}
		
		//Start Flickering only text to make a simple animations
		if (!m_start && m_flash)
		{
			m_timeAlpha -= Time.deltaTime * k_velFlahsingText;
			m_textButton.color = new Color(m_textButton.color.r,m_textButton.color.g,m_textButton.color.b,m_timeAlpha);
			m_textButton.Commit();
			if(m_timeAlpha < 0.1f)
				m_flash = false;
		}else
		{
			if (!m_start && !m_flash)
			{
				m_timeAlpha += Time.deltaTime * k_velFlahsingText;
				m_textButton.color = new Color(m_textButton.color.r,m_textButton.color.g,m_textButton.color.b,m_timeAlpha);
				m_textButton.Commit();
				if(m_timeAlpha >= 1.0f)
					m_flash = true;
			}
		}
	}
}
