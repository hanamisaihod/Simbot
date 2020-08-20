using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerController : MonoBehaviour
{
	public AudioMixer mixer;
	public Slider bgmSlider;
	public Slider robotSlider;
	public Slider sfxSlider;

	public void setMusicVolume(float volume)
	{
		mixer.SetFloat("musicVolume", volume);
	}
	public void setRobotVolume(float volume)
	{
		mixer.SetFloat("robotVolume", volume);
	}
	public void setEffectVolume(float volume)
	{
		mixer.SetFloat("effectVolume", volume);
	}

	void Start()
	{
		bgmSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
		robotSlider.value = PlayerPrefs.GetFloat("robotVolume", 0);
		sfxSlider.value = PlayerPrefs.GetFloat("effectVolume", 0);
	}

	public void SaveAudioSetting()
	{
		float musicVolume = 0;
		float robotVolume = 0;
		float effectVolume = 0;

		mixer.GetFloat("musicVolume", out musicVolume);
		mixer.GetFloat("robotVolume", out robotVolume);
		mixer.GetFloat("effectVolume", out effectVolume);

		PlayerPrefs.SetFloat("musicVolume", musicVolume);
		PlayerPrefs.SetFloat("robotVolume", robotVolume);
		PlayerPrefs.SetFloat("effectVolume", effectVolume);
		PlayerPrefs.Save();
	}
}
