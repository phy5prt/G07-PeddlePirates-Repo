using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMTEST : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//could use prefabs and automatic loop games so that once woody set up type of game, it will keep going through and maybe can keep scores for the scenario best scores of the scenario


	/*

	using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;



void Start(){

//I will use this for splash
		if(0 == SceneManager.GetActiveScene().buildIndex){


//changes to zero if i use a public float instead of  a number

		Invoke("LoadNextLevel", 3f);

		}

		/* 
		if (instance != null && instance != this) {
			Destroy (gameObject);
			// print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
				}
		


}




	public void LoadLevel(string name){


//		Debug.Log ("New Level load: " + name);
	
		SceneManager.LoadScene(name);

	}

	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel() {
		
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

	}
	




	private int indexCheck;
private AudioSource audioSource;

public AudioClip[] levelMusicChangeArray;


	
	// Update is called once per frame
	void Awake () {
	DontDestroyOnLoad (gameObject);
	}


	void Start() {

	audioSource = GetComponent<AudioSource>();


	}

	void OnLevelWasLoaded(int level){

	AudioClip thisLevelMusic = levelMusicChangeArray[level];


	Debug.Log("Playing clip" + levelMusicChangeArray[level]);



	if(thisLevelMusic){



			if ( PlayersPrefsManager.GetMasterVolume() != null){


			audioSource.volume = PlayersPrefsManager.GetMasterVolume();}


			else{audioSource.volume = 0.5f;}


	audioSource.clip = thisLevelMusic;
	audioSource.loop = true;
	audioSource.Play();
	}



	}


}










	*/






}



