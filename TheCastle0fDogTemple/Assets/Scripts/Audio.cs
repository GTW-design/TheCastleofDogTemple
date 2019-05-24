using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {

    public AudioSource[] audioSources = new AudioSource[2];
	
	// Update is called once per frame
	void Play_1()
    {
        audioSources[0].Play();
    }
    void Play_2()
    {
        audioSources[1].Play();
    }
}
