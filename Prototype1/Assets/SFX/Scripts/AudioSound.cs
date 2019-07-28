using UnityEngine.Audio;
using UnityEngine;

//To enable the custom class (AudioClass) to be editable from the inspector
[System.Serializable]
public class AudioSound
{
    public AudioClip soundClip;//sound object/clip
    public string name; //name of audio

    [Range(0.1f, 1f)] //Volume Slider
    public float volume;

    [Range(0.1f, 3f)] //Pitch slider
    public float pitch;

    public bool loop;

    //Play Audio
    [HideInInspector]//don't show in inspector because it's made public in Awake method
    public AudioSource source;

    
    
}
