using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    public bool IsAudioOn
    {
        get => PlayerPrefs.GetInt("SoundOn", 1) == 1;
        set
        {
            if (!value)
            {
                audioSources.ForEach(x => x.Stop());
            }

            PlayerPrefs.SetInt("SoundOn", value == true ? 1 : 0);
        }
    }
    
    private List<AudioItem> audioItems;
    private List<AudioSource> audioSources;
    [SerializeField] private AudioSource bgmSource;

    private void Awake()
    {
        instance = this;
        audioSources = new List<AudioSource>();
        audioItems = Resources.LoadAll<AudioItem>("Audio").ToList();
    }
    
    private void Start()
    {
        var music = PlayerPrefs.GetInt("MusicOn", 1);
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource != null)
        {
            bgmSource.mute = music == 0;
            bgmSource.Play();
        }
    }
    
    public void PlayAudioIfNotPlaying(AudioType audioType, bool loop = false)
    {
        if (IsAudioPlaying(audioType)) return;
        PlayAudio(audioType, loop);
    }
    
    public void PlayAudio(AudioType audioType, bool loop = false)
        {
            if (!IsAudioOn)
                return;

            var source = GetAvailableSource();
            var clip = audioItems.FirstOrDefault(x => x.AudioType == audioType);
            if (clip == null)
                return;

            source.pitch = 1f;
            source.clip = clip.AudioClip;
            source.volume = clip.Volume;
            source.loop = loop;
            source.time = 0;
            source.Play();
        }

    public void PlayAudioIfNotPlaying(AudioType audioType, float pitch, bool loop = false)
        {
            if (IsAudioPlaying(audioType)) return;
            PlayAudio(audioType, pitch, loop);
        }

    public void PlayAudio(AudioType audioType, float pitch, bool loop = false)
        {
            if (!IsAudioOn)
                return;

            var source = GetAvailableSource();
            var clip = audioItems.FirstOrDefault(x => x.AudioType == audioType);
            if (clip == null)
                return;

            source.pitch = pitch;
            source.clip = clip.AudioClip;
            source.volume = clip.Volume;
            source.loop = loop;
            source.time = 0;
            source.Play();
        }

    public void PlayAudioIfNotPlaying(AudioType audioType, bool loop, float time)
        {
            if (IsAudioPlaying(audioType)) return;
            PlayAudio(audioType, loop, time);
        }

    public void PlayAudio(AudioType audioType, bool loop, float time)
        {
            if (!IsAudioOn)
                return;

            var source = GetAvailableSource();
            var clip = audioItems.FirstOrDefault(x => x.AudioType == audioType);
            if (clip == null)
                return;


            source.clip = clip.AudioClip;
            source.volume = clip.Volume;
            var playTime = clip.AudioClip.length * time;
            source.loop = loop;
            source.time = playTime;
            source.Play();
        }

    public bool IsAudioPlaying(AudioType type)
        {
            var clip = audioItems.FirstOrDefault(x => x.AudioType == type);
            if (clip == null)
                return false;

            return audioSources.FirstOrDefault(x => x.clip == clip.AudioClip && x.isPlaying) != null;
        }

    public void StopAudio(AudioType audioType)
        {
            var clip = audioItems.FirstOrDefault(x => x.AudioType == audioType);
            if (clip == null)
                return;

            AudioSource audioSource = GetComponent<AudioSource>();
            var sources = audioSources.Where(x => x.clip == clip.AudioClip).ToList();
            Debug.Log("StopAudio: " + audioType + " count: " + sources.Count);
            sources.ForEach(x => x.Stop());
        }

    private AudioSource GetAvailableSource()
        {
            var available = audioSources.FirstOrDefault(x => !x.isPlaying);
            if (available == null)
            {
                available = gameObject.AddComponent<AudioSource>();
                audioSources.Add(available);
            }

            return available;
        }

    public void SetMusicEnabled()
        {
            var music = PlayerPrefs.GetInt("MusicOn");

            if (bgmSource != null)
            {
                bgmSource.mute = music == 0;
            }
        }
}

public enum AudioType
{
    None = 1,
    LevelSuccess = 25,
    LevelFailed = 30,
    Button = 35,
    Footsteps,
    VineClimbing,
    Throwing,
    Hitting,
    Burning,
    WaterSplash,
    Falling,
    Damage,
    Grabbing,
    Jumping,
    Landing,
    Alert,
    GameMusic,
    Ambient

}
