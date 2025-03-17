using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace WinterUniverse
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioSource _ambientSource;
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private float _ambientFadeSpeed = 0.5f;
        [SerializeField] private List<AudioClip> _ambientClips = new();
        [SerializeField] private AudioClip _errorClip;
        [SerializeField, Range(0f, 1f)] private float _defaultMasterVolume = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _defaultAmbientVolume = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _defaultSoundVolume = 0.5f;

        private Coroutine _changeAmbientCoroutine;

        public AudioClip ErrorClip => _errorClip;

        public void InitializeComponent()
        {
            SetMasterVolume(PlayerPrefs.GetFloat("VolumeMaster", _defaultMasterVolume));
            SetAmbientVolume(PlayerPrefs.GetFloat("VolumeAmbient", _defaultAmbientVolume));
            SetSoundVolume(PlayerPrefs.GetFloat("VolumeSound", _defaultSoundVolume));
            ChangeAmbient();
        }

        public void SetMasterVolume(float value)
        {
            _audioMixer.SetFloat("VolumeMaster", value);
            PlayerPrefs.SetFloat("VolumeMaster", value);
        }

        public void SetAmbientVolume(float value)
        {
            _audioMixer.SetFloat("VolumeAmbient", value);
            PlayerPrefs.SetFloat("VolumeAmbient", value);
        }

        public void SetSoundVolume(float value)
        {
            _audioMixer.SetFloat("VolumeSound", value);
            PlayerPrefs.SetFloat("VolumeSound", value);
        }

        public static AudioClip ChooseRandomClip(List<AudioClip> clips)
        {
            if (clips == null || clips.Count == 0)
            {
                return null;
            }
            return clips[Random.Range(0, clips.Count)];
        }

        public void PlaySound(AudioClip clip, bool randomizePitch = true, float volume = 1f, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if (clip == null)
            {
                _soundSource.volume = 1f;
                _soundSource.pitch = 1f;
                _soundSource.PlayOneShot(_errorClip);
                return;
            }
            _soundSource.volume = volume;
            _soundSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;
            _soundSource.PlayOneShot(clip);
        }

        public void ChangeAmbient()
        {
            ChangeAmbient(_ambientClips);
        }

        public void ChangeAmbient(List<AudioClip> clips)
        {
            if (_changeAmbientCoroutine != null)
            {
                StopCoroutine(_changeAmbientCoroutine);
            }
            _changeAmbientCoroutine = StartCoroutine(PlayAmbientCoroutine(clips));
        }

        public void StopAmbient()
        {
            if (_changeAmbientCoroutine != null)
            {
                StopCoroutine(_changeAmbientCoroutine);
                _changeAmbientCoroutine = null;
            }
            StartCoroutine(StopAmbientCoroutine());
        }

        private IEnumerator PlayAmbientCoroutine(List<AudioClip> clips)
        {
            WaitForSeconds delay = new(5f);
            while (true)
            {
                while (_ambientSource.volume != 0f)
                {
                    _ambientSource.volume -= _ambientFadeSpeed * Time.deltaTime;
                    yield return null;
                }
                _ambientSource.volume = 0f;
                _ambientSource.clip = clips[Random.Range(0, clips.Count)];
                _ambientSource.Play();
                while (_ambientSource.volume != 1f)
                {
                    _ambientSource.volume += _ambientFadeSpeed * Time.deltaTime;
                    yield return null;
                }
                _ambientSource.volume = 1f;
                while (_ambientSource.isPlaying)
                {
                    yield return delay;
                }
            }
        }

        private IEnumerator StopAmbientCoroutine()
        {
            while (_ambientSource.volume != 0f)
            {
                _ambientSource.volume -= _ambientFadeSpeed * Time.deltaTime;
                yield return null;
            }
            _ambientSource.volume = 0f;
        }
    }
}