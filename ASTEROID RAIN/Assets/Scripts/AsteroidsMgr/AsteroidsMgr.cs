/*****************************************************************************
*                                                                            *
*  Asteroid Rain!                                                            *
*  Sergi Herrero Collada.   												 *
*  2013                              										 *
*                                                                            *
*****************************************************************************/
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidsMgr : MonoBehaviour {
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// UNITY OBJECT PARAMETERS
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public string[] m_nameAsteroidsPrefabs  = {"Asteroid1","Asteroid2","Asteroid3","Asteroid4"};
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CONSTANT  VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// MEMBER VARIABLES
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private int m_numMaxAsteroidsInScreen = 15;
	private List<GameObject> m_asteroidsList;
	private float m_posYToCreate = 0;
	private int m_totalAsteroidsInScreen = 0;
	private string m_nameBigAsteroidPrefab = "AsteroidBig";
	private bool m_asteroidsCanBeDestroyed = true;
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// SINGLETON INSTANCE
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private static AsteroidsMgr s_instance = null;
	public static AsteroidsMgr Instance
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
		
		//Initialize List of Asteroids
		m_asteroidsList = new List<GameObject>();
		
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// Use this for initialization
	// ------------------------------------------------------------------------------------------------------------------------------------------
	void Start () 
	{
		//Get this gameobject available also in future scenes
		DontDestroyOnLoad(gameObject);
		
		//Initialize the m_asteroid to get Y size of the sprite to calculate the Initial Position the Asteroids Are 
		//going to be instantiated
		GameObject actualAsteroid = (GameObject)Resources.Load(m_nameAsteroidsPrefabs[0]);
		m_posYToCreate = Camera.mainCamera.transform.localPosition.y + Camera.mainCamera.orthographicSize + actualAsteroid.GetComponent<tk2dSprite>().GetBounds().size.y;
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CreateRandomAsteroid: Create a Random Asteroid in a Random X position in the screen
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void CreateRandomAsteroid ()
	{	if(m_totalAsteroidsInScreen < m_numMaxAsteroidsInScreen)
		{
			// Calculate the  X random position in the screen to Instantiate an Object
			float Xposition = Global.RandomXposition();
			
			//Calculate Randomly the type of Asteroid is going to be generated
			Random.seed++;
			int posTypeAsteroid = Random.Range(0,m_nameAsteroidsPrefabs.Length);
			string nameTypeAsteroid = m_nameAsteroidsPrefabs[posTypeAsteroid];
			CreateSpecificAsteroid(nameTypeAsteroid, new Vector3(Xposition,m_posYToCreate,0));
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CreateRandomAsteroid: Create a Random Asteroid in concrete position of the screen
	// ------------------------------------------------------------------------------------------------------------------------------------------
	
	public void CreateRandomAsteroid (Vector3 position)
	{			
		//Calculate Randomly the type of Asteroid is going to be generated
		Random.seed++;
		int posTypeAsteroid = Random.Range(0,m_nameAsteroidsPrefabs.Length);
		string nameTypeAsteroid = m_nameAsteroidsPrefabs[posTypeAsteroid];
		CreateSpecificAsteroid(nameTypeAsteroid, position);
	}
			
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CreateSpecificAsteroid: Instantiate a specific Asteroid
	// ------------------------------------------------------------------------------------------------------------------------------------------
	private void CreateSpecificAsteroid (string typeOfAsteroid, Vector3 position)
	{
		//create an specific Asteroid (1,2,3,4 or Big)
		GameObject asteroid = (GameObject)Instantiate(Resources.Load(typeOfAsteroid));
		asteroid.transform.parent = transform;
		asteroid.transform.localPosition = new Vector3(position.x,position.y,asteroid.transform.localPosition.z);
		asteroid.GetComponent<AsteroidBase>().CanBeDestroyed = m_asteroidsCanBeDestroyed;
		
		m_asteroidsList.Add(asteroid);
		m_totalAsteroidsInScreen++;
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// DeleteOneAsteroidFromList: Remove one received Asteroid From the List
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void DeleteOneAsteroidFromList (GameObject asteroid)
	{
		m_asteroidsList.Remove(asteroid);
		Destroy(asteroid);
		m_totalAsteroidsInScreen--;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// DeleteAllAsteroidFromList: Remove All Asteroid From the List
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void DeleteAllAsteroidFromList ()
	{
		//Destroy each gameobject in the list
		foreach(GameObject asteroid in m_asteroidsList)
		{
			Destroy(asteroid);
		}	
		
		//remove all asteroids from the List
		m_asteroidsList.Clear();
		
		m_totalAsteroidsInScreen = 0;
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// StartThrowingAsteroids: Starts the Coroutine of Throwing Asteroids
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void StartThrowingAsteroids (float timeBetweenAstorids, bool asteroidsCanBeDestroyed)
	{
		m_asteroidsCanBeDestroyed = asteroidsCanBeDestroyed;
		StartCoroutine("CreateRandomAsteroids",timeBetweenAstorids);
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  CreateRandomAsteroids: create random Asteroids every X seconds to make an animation 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public IEnumerator CreateRandomAsteroids(float timeBetweenAstorids)
	{
		while(true)
		{
			//Create an Asteroid
			CreateRandomAsteroid();
          	yield return new WaitForSeconds(timeBetweenAstorids);
     	}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  StopCreateAsteroids: When Finished game stop creating new Asteroids 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void StopCreateAsteroids()
	{
		StopCoroutine("CreateRandomAsteroids");
		StopCoroutine("CreateBigAsteroids");
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// NumMaximumAsteroids: Get/Set the max of asteroids shown at the same time
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public int NumMaximumAsteroids
	{
		get { return m_numMaxAsteroidsInScreen; }
		set
		{
			if (m_numMaxAsteroidsInScreen != value)
			{
				m_numMaxAsteroidsInScreen = value;
			}
		}
	}
	
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// StartThrowingBigAsteroids: Starts the Coroutine of Throwing Big Asteroids
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void StartThrowingBigAsteroids (float timeBetweenBigAstorids)
	{
		StartCoroutine("CreateBigAsteroids",timeBetweenBigAstorids);
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	//  CreateRandomAsteroids: create random Asteroids every X seconds to make an animation 
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public IEnumerator CreateBigAsteroids(float timeBetweenBigAstorids)
	{
		while(true)
		{
			//Create a Big Asteroid
			CreateBigAsteroid();
          	yield return new WaitForSeconds(timeBetweenBigAstorids);
     	}
	}
	// ------------------------------------------------------------------------------------------------------------------------------------------
	// CreateBigAsteroid: Create a Big Asteroid in a Random X position in the screen
	// ------------------------------------------------------------------------------------------------------------------------------------------
	public void CreateBigAsteroid ()
	{	
		// Calculate the  X random position in the screen to Instantiate an Object
		float Xposition = Global.RandomXposition();
		
		CreateSpecificAsteroid(m_nameBigAsteroidPrefab, new Vector3(Xposition,m_posYToCreate,0));
		
	}
}
