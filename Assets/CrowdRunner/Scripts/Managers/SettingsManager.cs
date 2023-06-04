using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] SoundsManager soundsManager;
    [SerializeField] VibrationManager vibrationManager;

    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image vibrationsButtonImage;

    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;

    private bool soundsState = true;
    private bool vibrationsState = true;

    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("Sounds") == 1;
        vibrationsState = PlayerPrefs.GetInt("Vibrations") == 1;
    }

    private void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if (soundsState)
            EnableSounds();
        else
            DisableSounds();

        if (vibrationsState)
            EnableVibrations();
        else
            DisableVibrations();
    }

    public void ChangeSoundsState()
    {
        if (soundsState)
            DisableSounds();
        else
            EnableSounds();

        soundsState = !soundsState;

        PlayerPrefs.SetInt("Sounds", soundsState ? 1 : 0);
    }

    public void ChangeVibrationsState()
    {
        if (vibrationsState)
            DisableVibrations();
        else
            EnableVibrations();

        vibrationsState = !vibrationsState;

        PlayerPrefs.SetInt("Vibrations", vibrationsState ? 1 : 0);
    }

    private void EnableSounds()
    {
        soundsManager.EnableSounds();

        soundsButtonImage.sprite = optionsOnSprite;
    }

    private void DisableSounds()
    {
        soundsManager.DisableSounds();

        soundsButtonImage.sprite = optionsOffSprite;
    }

    private void DisableVibrations()
    {
        vibrationManager.DisableVibrations();

        vibrationsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableVibrations()
    {
        vibrationManager.EnableVibrations();

        vibrationsButtonImage.sprite = optionsOnSprite;
    }
}
