using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;
using FMODUnity;

///
/// Author: Samuel Müller: sm184
/// Description: Manages volumes.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class VolumeMenu : MonoBehaviour
{
    private Bus _sfx;
    private Bus _dialog;
    [SerializeField] private VolumeStats _volumes = new VolumeStats(1,1,1,1);
    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _dialogSlider;
    [SerializeField] private Slider _musicSlider;

    public VolumeStats Volumes
    {
        get => _volumes; set => _volumes = value;
    }

    public float MasterVolume
    {
        set
        {
            _volumes.Master = value;
            MapVolumesByValues();
        }
    }
    public float MusicVolume
    {
        set
        {
            _volumes.Music = value;
            MapVolumesByValues();
        }
    }

    public float SFXVolume
    {
        set
        {
            _volumes.SFX = value;
            MapVolumesByValues();
        }
    }

    public float DialogVolume
    {
        set
        {
            _volumes.Dialog = value;
            MapVolumesByValues();
        }
    }
    private void Awake()
    {
        _sfx = RuntimeManager.GetBus("bus:/SFX");
        _dialog = RuntimeManager.GetBus("bus:/Voices");
        MapSlidersByVolumes();
    }

    public void MapSlidersByVolumes()
    {
        _masterSlider.value = _volumes.Master;
        _sfxSlider.value = _volumes.BaseSFX;
        _musicSlider.value = _volumes.BaseMusic;
        _dialogSlider.value = _volumes.BaseDialog;
    }

    public void MapVolumesByValues()
    {
        GameObject.Find("EliasMusicPlayer").GetComponent<AudioSource>().volume = _volumes.Music;
            _sfx.setVolume(_volumes.SFX);
            _dialog.setVolume(_volumes.Dialog);
    }

    [System.Serializable]
    public class VolumeStats
    {
        [SerializeField]
        private float _master, _sfx, _music, _dialog;

        public float Master { get => _master; set => _master = value; }

        public float SFX { get => _master * _sfx; set => _sfx = value; }

        public float Music { get => _master * _music; set => _music = value; }
        public float Dialog { get => _master * _dialog; set => _dialog = value; }
        public float BaseSFX { get => _sfx; set => _sfx = value; }
        public float BaseDialog { get => _dialog; set => _dialog = value; }
        public float BaseMusic { get => _music; set => _music = value; }

        public VolumeStats(float master, float sfx, float music, float dialog)
        {
            _master = master;
            _sfx = sfx;
            _music = music;
            _dialog = dialog;
        }
    }

}
