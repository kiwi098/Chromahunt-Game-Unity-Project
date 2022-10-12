
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private static SoundManager instance = null;
    public static SoundManager Instance
    {
    get{ return instance;} 
    }


    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance= this;
        }
        DontDestroyOnLoad(this.gameObject);


        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }

    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }


}
