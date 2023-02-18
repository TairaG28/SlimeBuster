using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//Audio�Ǘ��N���X�@�V�[�����܂����ł��j������Ȃ��V���O���g���Ŏ�������
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
            //���łɃC���X�^���X������ꍇ�͎��g��j������
            Destroy(gameObject);
            return;
        }

        //�V�[����J�ڂ��Ă��j������Ȃ�����
        DontDestroyOnLoad(gameObject);

        //�C���X�^���X�Ƃ��ĕێ�����
        instance = this;

        //Resources/2D_SE�f�B���N�g������AudioClip��S�Ď擾����
        var audioClips = Resources.LoadAll<AudioClip>("SouondEffects");
        foreach (var clip in audioClips)
        {
            //AudioClip��Dictionary�ɕێ����Ă���
            _clips.Add(clip.name, clip);
        }

    }



    public void PlayBGM(string clipName)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //���݂��Ȃ����O���w�肵����G���[
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //�w��̖��O��clip�ɍ��ւ��čĐ�����B
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups("BGM")[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = 1.0f;
        _audioSource.Play();

    }


    //�w�肵�����O�̉����t�@�C�����Đ�����
    public void Play(string clipName, string groups)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //���݂��Ȃ����O���w�肵����G���[
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //�w��̖��O��clip�ɍ��ւ��čĐ�����B
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups(groups)[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = 1.0f;
        _audioSource.PlayOneShot(_audioSource.clip);

    }

    //�w�肵�����O�̉����t�@�C�����s�b�`��ς��čĐ�����
    public void PlayChangePitch (string clipName , float pitch, string groups)
    {
        if (!_clips.ContainsKey(clipName))
        {
            //���݂��Ȃ����O���w�肵����G���[
            throw new Exception("Sound" + clipName + "is not defined");
        }

        //�w��̖��O��clip�ɍ��ւ��čĐ�����B
        
        _audioSource.outputAudioMixerGroup = _audioMixier.FindMatchingGroups(groups)[0];
        _audioSource.clip = _clips[clipName];
        _audioSource.pitch = pitch;
        _audioSource.PlayOneShot(_audioSource.clip);
        
    }


}
