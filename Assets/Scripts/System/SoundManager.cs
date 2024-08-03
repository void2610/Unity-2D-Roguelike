namespace NManager
{
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Audio;

    public class SoundManager : MonoBehaviour
    {
        [System.Serializable]
        public class SoundData
        {
            public string name;
            public AudioClip audioClip;
            public float volume = 1.0f;
        }

        [SerializeField]
        private AudioMixerGroup seMixerGroup;
        [SerializeField]
        private SoundData[] soundDatas;



        public static SoundManager instance;
        private AudioSource[] seAudioSourceList = new AudioSource[20];
        private float seVolume = 0.5f;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }

            for (var i = 0; i < seAudioSourceList.Length; ++i)
            {
                seAudioSourceList[i] = gameObject.AddComponent<AudioSource>();
                seAudioSourceList[i].outputAudioMixerGroup = seMixerGroup;
            }
        }

        public float SeVolume
        {
            get
            {
                return seVolume;
            }
            set
            {
                seVolume = value;
                PlayerPrefs.SetFloat("SeVolume", value);
            }
        }

        public void PlaySe(AudioClip clip, float volume = 1.0f, float pitch = 1.0f)
        {
            var audioSource = GetUnusedAudioSource();
            if (clip == null)
            {
                Debug.LogWarning("指定されたAudioClipが存在しません。");
                return;
            }
            if (audioSource == null)
            {
                Debug.LogWarning("再生可能なAudioSourceがありません。");
                return;
            }

            audioSource.clip = clip;
            audioSource.volume = volume * SeVolume;
            audioSource.pitch = pitch;
            audioSource.Play();
        }

        public void PlaySe(string name, float volume = 1.0f, float pitch = 1.0f)
        {
            var soundData = soundDatas.FirstOrDefault(t => t.name == name);
            var audioSource = GetUnusedAudioSource();
            if (soundData == null)
            {
                Debug.LogWarning("指定された名前のSEが存在しません。");
                return;
            }
            if (audioSource == null)
            {
                Debug.LogWarning("再生可能なAudioSourceがありません。");
                return;
            }

            audioSource.clip = soundData.audioClip;
            audioSource.volume = soundData.volume * volume * SeVolume;
            audioSource.pitch = pitch;
            audioSource.Play();
        }

        private AudioSource GetUnusedAudioSource() => seAudioSourceList.FirstOrDefault(t => t.isPlaying == false);

        private void Start()
        {
            SeVolume = 0.5f;
        }
    }
}
