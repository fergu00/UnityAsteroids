/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;

public class SoundMgr : MonoBehaviour 
{

	// ------------------------------------------------------------------------------------------------------------------------------------------
	// PUBLIC UNITY PARAMETERS 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public AudioClip[] m_MainThemeSounds;
	public AudioClip[] m_FXThemeSounds;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER CONSTANTS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public enum MusicSounds 
	{
		k_main_theme,
		//k_main_theme1,
	}
	
	public enum FXSounds 
	{
		explote,
		lossLive
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private AudioSource m_audioSourceMainTheme;
	private AudioSource m_audioSourceFX;
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// SINGLETON INSTANCE
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private static SoundMgr s_instance = null;
	public static SoundMgr Instance
	{
		get
		{
			return s_instance;
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Awake ()
	{
		// set singleton
		s_instance = this;
		
		// Initialize AudiSoice for a non loop and loop audio - Main THEME
		m_audioSourceMainTheme = gameObject.AddComponent<AudioSource>();
		m_audioSourceMainTheme.audio.playOnAwake = false;
		m_audioSourceMainTheme.audio.volume = 0.4f;
		m_audioSourceMainTheme.audio.loop = true;
		
		// Initialize AudiSoice for a non loop FX audio
		m_audioSourceFX = gameObject.AddComponent<AudioSource>();
		m_audioSourceFX.audio.playOnAwake = false;
		m_audioSourceFX.audio.volume = 1.0f;
		
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{		
		DontDestroyOnLoad(gameObject);
		
		// Initialize Main theme Music!
		PlaySound(MusicSounds.k_main_theme);
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Update is called once per frame
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Update () 
	{
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  Play Normal music backgrund looped
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void PlaySound (MusicSounds soundId)
	{		
		m_audioSourceMainTheme.audio.volume = 0.4f;
		m_audioSourceMainTheme.audio.clip = m_MainThemeSounds[(int) soundId];
		m_audioSourceMainTheme.audio.Play();
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  Play FX Sound
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void PlaySoundFX (FXSounds soundId)
	{		
		//Playing one shot We play new sound each time we call it
		m_audioSourceFX.PlayOneShot(m_FXThemeSounds[(int) soundId],1.0f);
	}
}
