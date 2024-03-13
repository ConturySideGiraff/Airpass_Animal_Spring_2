using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private List<AudioClip> _audioClips;
    [Space]
    [SerializeField] private AudioSource _narrationAudioSource;
    [Space]
    [SerializeField] private AudioSource _sfxAudioSource;
    [Space]
    [SerializeField] private AudioSource _bgmAudioSource;

    
    public float NarrationPlay(int contentIndex, out AudioSource narrationAudioSource)
    {
        AudioClip clip = contentIndex switch
        {
            0 => Get(AudioClipName.Narration0),
            _ => null
        };
        
        narrationAudioSource = _narrationAudioSource;
        narrationAudioSource.clip = clip;
        narrationAudioSource.Play();
        
        return clip.length;
    }

    public void NarrationPause()
    {
        _narrationAudioSource.Pause();
    }

    public void NarrationUnPause()
    {
        _narrationAudioSource.UnPause();
    }
    
    //

    public void SetVolumeSfx(int index)
    {
        // sfx 0 - 0.7 - 1.0

        _sfxAudioSource.volume = index switch
        {
            0 => 0.0f,
            1 => 0.7f,
            2 => 1.0f,
            _ => _sfxAudioSource.volume
        };
        
        _narrationAudioSource.volume = index switch
        {
            0 => 0.0f,
            1 => 0.7f,
            2 => 1.0f,
            _ => _narrationAudioSource.volume
        };
    }

    public void SetVolumeBgm(int index)
    {
        // bgm 0 - 0.4 - 0.7

        _bgmAudioSource.volume = index switch
        {
            0 => 0.0f,
            1 => 0.4f,
            2 => 0.7f,
            _ => _bgmAudioSource.volume
        };
    }

    private AudioClip Get(AudioClipName name)
    {
        return _audioClips.FirstOrDefault(c => c.name == name.ToString());
    }

    public void SfxPlay(AudioClipName name)
    {
        _sfxAudioSource.PlayOneShot(Get(name));
    }

    #region Editor_Only
#if UNITY_EDITOR
    [MenuItem("__CSG__/Enum/AudioClipNameUpdate")]
    public static void AudioEnumUpdate()
    {
        const string enumName = "AudioClipName";

        string pilPath = Path.Combine(Application.dataPath, "__Data", "Scripts", "PIL",$"{enumName}.cs");
        string dirPath = Path.Combine(Application.dataPath, "__Data", "Audios");
        
        StringBuilder stringBuilder = new StringBuilder().Append($"public enum {enumName} ").Append("{\n");
        
        foreach (string dir in Directory.GetFiles(dirPath))
        {
            string[] fileFullName = Path.GetFileName(dir).Split('.');

            if (fileFullName[^1] == "meta")
            {
                continue;
            }

            string fileName = fileFullName[0];
            
            stringBuilder.Append("  ").Append(fileName).Append(",").Append("\n");
        }
       
        stringBuilder.Append("}");
        
        if (File.Exists(pilPath))
        {
            File.Delete(pilPath);
        }
        
        File.WriteAllText(pilPath, stringBuilder.ToString());
        
        AssetDatabase.Refresh();
    }

    public void AudioUpdate()
    {
        string assetPath = Path.Combine("Assets", "__Data", "Audios");

        string[] names = Enum.GetNames(typeof(AudioClipName));
        
        _audioClips.Clear();
        
        foreach (string name in names)
        {
            string audioAssetPath = Path.Combine(assetPath, name);

            AudioClip clip = AssetDatabase.LoadAssetAtPath<AudioClip>(audioAssetPath);
            
            _audioClips.Add(clip);
        }
    }
#endif
    #endregion
}

// #region Editor_Only
// #if UNITY_EDITOR
// [CustomEditor(typeof(AudioManager)), CanEditMultipleObjects]
// public class AudioManagerEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//
//         if (GUILayout.Button("AudioClipUpdate"))
//         {
//             (target as AudioManager)?.AudioUpdate();
//         }
//     }
// }
// #endif
// #endregion
