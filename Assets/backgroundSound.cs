using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//preload sounds
//have gamemanager choose the backgroundsounds
//coroutine to fade in and out would be good

public class backgroundSound : MonoBehaviour {

public AudioClip[] backgroundSounds;
private AudioSource[] audioSources;
private bool audioBlendInprogress = false; //if issues use this by seeing if its true if it is cancel the coroutine and start a new one
[SerializeField] float myVolume = 0.2f;

	// Use this for initialization
	void Start () {
	audioSources = GetComponents<AudioSource>();
	foreach(AudioSource audioS in audioSources){audioS.volume = myVolume;}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playEventSetupSpound(){

	audioSources[0].clip = backgroundSounds[0];
	audioSources[1].clip = backgroundSounds[2];
	audioSources[0].Play();
	}

	public void playPlayerSetupSpound(){
	if(audioSources[1].isPlaying){
			StartCoroutine(CrossFadeAudio(audioSources[1], audioSources[0], 2f, myVolume));


	} //crossfade otherwise
	else{
	audioSources[0].clip = backgroundSounds[1];
	audioSources[0].Play();
	}

	}

	public void playGameBackGroundSound(){


		StartCoroutine(CrossFadeAudio(audioSources[0], audioSources[1], 2f, myVolume));
	//audioSources[1].clip = backgroundSounds[2];


	}



//----------------------------------
// AUDIO CROSSFADE
//----------------------------------
private IEnumerator CrossFadeAudio(AudioSource audioSource1, AudioSource audioSource2, float crossFadeTime, float audioSource2VolumeTarget)
{
    string debugStart = "<b><color=red>ERROR:</color></b> ";
    int maxLoopCount = 1;
    int loopCount = 0;
    float startAudioSource1Volume = audioSource1.volume;
 
    if(audioSource1 == null || audioSource2 == null)
    {
        Debug.Log(debugStart + transform.name + ".EngineControler.CrossFadeAudio recieved NULL value.\n*audioSource1=" + audioSource1.ToString() + "\n*audioSource2=" + audioSource2.ToString(), gameObject);
        yield return null;
    }
    else
    {
        audioBlendInprogress = true;
 
        audioSource2.volume = 0f;
        audioSource2.Play();
 
        while ((audioSource1.volume > 0f && audioSource2.volume < audioSource2VolumeTarget) && loopCount < maxLoopCount)
        {
            audioSource1.volume -= startAudioSource1Volume * Time.deltaTime / crossFadeTime;
            audioSource2.volume += audioSource2VolumeTarget * Time.deltaTime / crossFadeTime;
            loopCount++;
            yield return null;
        }
 
        if (loopCount < maxLoopCount)
        {
            audioSource1.Stop();
            audioSource1.volume = startAudioSource1Volume;
            audioBlendInprogress = false;
        }
        else
        {
            Debug.Log(debugStart + transform.name + ".EngineControler.CrossFadeAudio.loopCount reached max value.\nloopCount=" + loopCount + "\nmaxLoopCount=" + maxLoopCount, gameObject);
        }
    }
}


}
