using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioItem", menuName = "Audio Item", order = 0)]
public class AudioItem : ScriptableObject
{
    public AudioType AudioType;
    public AudioClip AudioClip;
    [Range(0, 1)] public float Volume = 1;
}
