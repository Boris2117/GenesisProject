using UnityEngine;
using UnityEngine.Audio;
public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer AudioMixer;

    public void setmastervolume (float level)
    {
        AudioMixer.SetFloat("Master", Mathf.Log10(level) * 20f);
    }
    public void setsoundfxvolume(float level)
    {
        AudioMixer.SetFloat("Sound", Mathf.Log10(level) * 20f);
    }
    public void setmusicvolume(float level)
    {
        AudioMixer.SetFloat("Music", Mathf.Log10(level) * 20f);
    }
}
