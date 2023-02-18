using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Audio管理クラス　シーンをまたいでも破棄されないシングルトンで実装する
public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private AudioMixer _audioMixier;
    [SerializeField] private AudioSource _audioSource;
    
    private readonly Dictionary<string, AudioClip> _clips =
        new Dictionary<string, AudioClip>();

    public static AudioManager Instance
    {
        get { return instance; }
    }


    private void Awake()
    {
        if (null != instance)
        {
            //すでにインスタンスがある場合は自身を破棄する
            Destroy(gameObject);
            return;
        }

        //シーンを遷移しても破棄されなくする
        DontDestroyOnLoad(gameObject);

        //インスタンスとして保持する
        instance = this;

        //Resources/2D_SEディレクトリ化のAudioClipを全て取得する
        var audioClips = Resources.LoadAll<AudioClip>("SouondEffects");
        foreach (var clip in audioClips)
        {
            //AudioClipをDictionaryに保持しておく
            _clips.Add(clip.name, clip);
        }

    }



    public void PlayBGM(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //存在しない名前を指定したらエラー
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //指定の名前のclipに差替えて再生する。
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups("BGM")[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = 1.0f;
        _audioSource.Play();

    }


    //指定した名前の音声ファイルを再生する
    public void Play(string clipName, string groups)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //存在しない名前を指定したらエラー
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //指定の名前のclipに差替えて再生する。
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups(groups)[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = 1.0f;
        _audioSource.PlayOneShot(_audioSource.clip);

    }

    //指定した名前の音声ファイルをピッチを変えて再生する
    public void PlayChangePitch (string clipName , float pitch, string groups)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //存在しない名前を指定したらエラー
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //指定の名前のclipに差替えて再生する。
        
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups(groups)[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(_audioSource.clip);
        
    }


}
