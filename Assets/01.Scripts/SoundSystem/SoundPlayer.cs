using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundManage
{

    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _sfxGroup, _musicGroup;
        public event Action<SoundPlayer> OnSoundPlayCompleteEvent;

        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        public void PlaySound(SoundSO data)
        {
            if (data.audioType == AudioType.SFX)
            {
                _audioSource.outputAudioMixerGroup = _sfxGroup;
            }
            else if (data.audioType == AudioType.BGM)
            {
                _audioSource.outputAudioMixerGroup = _musicGroup;
            }

            _audioSource.volume = data.volume;
            _audioSource.pitch = data.pitch;
            if (data.randomizePitch)
            {
                _audioSource.pitch += UnityEngine.Random.Range(-data.randomPitchModifier, data.randomPitchModifier);
            }
            _audioSource.clip = data.clip;

            _audioSource.loop = data.loop;

            _audioSource.PlayOneShot(data.clip);
            if (!data.loop)
            {
                float time = _audioSource.clip.length + 1.0f;
                StartCoroutine(DisableSoundTimer(time));
            }
        }

        private IEnumerator DisableSoundTimer(float time)
        {
            yield return new WaitForSeconds(time);
            OnSoundPlayCompleteEvent?.Invoke(this);
        }

        public void ResetItem()
        {
            _audioSource.Stop();
            
        }
    }

}