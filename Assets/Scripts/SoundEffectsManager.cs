using UnityEngine;

public class SoundEffectsManager : MonoBehaviour
{
    public static SoundEffectsManager instance;

    [SerializeField] private AudioSource SoundFxObject;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void PlaysoundFxclip(AudioClip audioClip, Transform spawntransform, float volume)
    {
        AudioSource audioSource = Instantiate(SoundFxObject, spawntransform.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();


        float cliplength = audioSource.clip.length;
        
        Destroy(audioSource.gameObject,cliplength);
    }

}
