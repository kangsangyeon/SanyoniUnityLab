using System;
using AudioConductor.Runtime.Core;
using AudioConductor.Runtime.Core.Models;
using UnityEngine;

public class AudioConductorExample : MonoBehaviour
{
    public string SettingsAddress;
    public string CueSheetAddress;
    public string CueName;

    private CueSheetAsset cueSheetAsset;

    void Start()
    {
        var settings = Resources.Load<AudioConductorSettings>(SettingsAddress);
        AudioConductorInterface.Setup(settings);

        cueSheetAsset = Resources.Load<CueSheetAsset>(CueSheetAddress);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            var _cueController = AudioConductorInterface.CreateController(cueSheetAsset, CueName);
            var _trackController = _cueController.Play();
            _trackController.AddStopAction(() => Debug.Log("Audio stopped."));
        }
    }
}