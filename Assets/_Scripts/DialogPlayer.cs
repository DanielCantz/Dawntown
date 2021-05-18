using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Random = UnityEngine.Random;

///
/// Author: Yannick Pfeifer yp009
/// Description: This is the dialog instance of the level and triggers audio and hud events
/// ==============================================
/// Changelog:
/// Daniel Cantz: Added dialog timings
/// ==============================================
///

public class DialogPlayer : MonoBehaviour
{
    private SoundContainer _soundContainer;
    private AudioSource _soundSource;
    private EnemyGameManager _enemyGameManager;
    private PopUpManager _hudCanvas;
    private LanguageManager _languageManager;

    [SerializeField] float minimumSoundWaitTimeInSeconds = 10f;
    [SerializeField] float maximumSoundWaitTimeInSeconds = 20f;

    private float _voiceLineCooldown;
    private bool _isPeace;
    private bool isGerman;
    
    private int _dialogCounter;
    private int _randomSound;
    private int _soundScene;
    
    [FMODUnity.EventRef]
    public string fmodEstellaTalkEvent, fmodTimTalkEvent;

    private GameObject _player;

    FMOD.Studio.EVENT_CALLBACK dialogueCallback;
    
    void Start()
    {
        _soundContainer = SoundContainer.Instance;
        _soundSource = _soundContainer.GetComponent<AudioSource>();
        _enemyGameManager = EnemyGameManager.Instance;
        _languageManager = LanguageManager.Instance;
        _hudCanvas = GameObject.FindWithTag("HUD").GetComponent<PopUpManager>();
        _player = GameObject.FindWithTag("Player");
        dialogueCallback = DialogueEventCallback;
        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        _hudCanvas.DisableDialog();
        _voiceLineCooldown = Random.Range(minimumSoundWaitTimeInSeconds, maximumSoundWaitTimeInSeconds);
        yield return new WaitForSeconds(_voiceLineCooldown);
        pickRandomSound();
    }
    void PlayDialogue(string key, soundRole role)
    {
        FMOD.Studio.EventInstance dialogueInstance;
        switch (role)
        {
            case soundRole.Estella:
                dialogueInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEstellaTalkEvent);
                dialogueInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(_player, _player.GetComponent<Rigidbody>()));
                break;
            case soundRole.Tim:
                dialogueInstance = FMODUnity.RuntimeManager.CreateInstance(fmodTimTalkEvent);
                break;
            default:
                dialogueInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEstellaTalkEvent);
                break;
        }

        // Pin the key string in memory and pass a pointer through the user data
        GCHandle stringHandle = GCHandle.Alloc(key, GCHandleType.Pinned);
        dialogueInstance.setUserData(GCHandle.ToIntPtr(stringHandle));
        dialogueInstance.setCallback(dialogueCallback);
        dialogueInstance.start();
        dialogueInstance.release();
    }
    
    public IEnumerator PlayVoiceLine()
    {
        getLanguage();
        
        if (isGerman)
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].subtileGer, _soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].role);
        }
        else
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].subtileEng, _soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].role);
        }
        
        PlayDialogue(_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].lineID,_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].role );
        
        if (_dialogCounter == 0)
        {
            _hudCanvas.EnableDialog();
        }
        yield return new WaitForSeconds(_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].lineAudio.length + _soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].waitTimeInMS / 1000);
        
        if (_soundContainer.soundScenes[_soundScene].soundEvents[_randomSound].soundLines[_dialogCounter].nextLineID == "End")
        {
            StartCoroutine(Cooldown());
        }
        else
        {
            _dialogCounter++;
            StartCoroutine(PlayVoiceLine());
        }
        
    }

    private void pickRandomSound()
    {
        _dialogCounter = 0;
        _isPeace = isPlayerPeaceful();
        
        if (_isPeace)
        {
            _soundScene = 1;
            _randomSound = Random.Range(0, _soundContainer.soundScenes[_soundScene].soundEvents.Length);
        }
        else
        {
            _soundScene = 2;
            _randomSound = Random.Range(0, _soundContainer.soundScenes[_soundScene].soundEvents.Length);
        }
        
        StartCoroutine(PlayVoiceLine());
    }

    private bool isPlayerPeaceful()
    {
        if (_enemyGameManager.aggroEnemys == 0)
        {
            return true;    
        }
        return false;
    }
    
    private void getLanguage()
    {
        switch (_languageManager.Language)
        {
            case LanguageEnum.English:
                isGerman = false;
                break;
            case LanguageEnum.German:
                isGerman = true;
                break;
        }
    }
    
    static FMOD.RESULT DialogueEventCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, FMOD.Studio.EventInstance instance, IntPtr parameterPtr)
    {
        IntPtr stringPtr;
        instance.getUserData(out stringPtr);

        GCHandle stringHandle = GCHandle.FromIntPtr(stringPtr);
        String key = stringHandle.Target as String;

        switch (type)
        {
            case FMOD.Studio.EVENT_CALLBACK_TYPE.CREATE_PROGRAMMER_SOUND:
                {
                    FMOD.MODE soundMode = FMOD.MODE.LOOP_NORMAL | FMOD.MODE.CREATECOMPRESSEDSAMPLE | FMOD.MODE.NONBLOCKING;
                    var parameter = (FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES));

                    if (key.Contains("."))
                    {
                        FMOD.Sound dialogueSound;
                        var soundResult = FMODUnity.RuntimeManager.CoreSystem.createSound(Application.streamingAssetsPath + "/" + key, soundMode, out dialogueSound);
                        if (soundResult == FMOD.RESULT.OK)
                        {
                            parameter.sound = dialogueSound.handle;
                            parameter.subsoundIndex = -1;
                            Marshal.StructureToPtr(parameter, parameterPtr, false);
                        }
                    }
                    else
                    {
                        FMOD.Studio.SOUND_INFO dialogueSoundInfo;
                        var keyResult = FMODUnity.RuntimeManager.StudioSystem.getSoundInfo(key, out dialogueSoundInfo);
                        if (keyResult != FMOD.RESULT.OK)
                        {
                            break;
                        }
                        FMOD.Sound dialogueSound;
                        var soundResult = FMODUnity.RuntimeManager.CoreSystem.createSound(dialogueSoundInfo.name_or_data, soundMode | dialogueSoundInfo.mode, ref dialogueSoundInfo.exinfo, out dialogueSound);
                        if (soundResult == FMOD.RESULT.OK)
                        {
                            parameter.sound = dialogueSound.handle;
                            parameter.subsoundIndex = dialogueSoundInfo.subsoundindex;
                            Marshal.StructureToPtr(parameter, parameterPtr, false);
                        }
                    }
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROY_PROGRAMMER_SOUND:
                {
                    var parameter = (FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(FMOD.Studio.PROGRAMMER_SOUND_PROPERTIES));
                    var sound = new FMOD.Sound();
                    sound.handle = parameter.sound;
                    sound.release();
                }
                break;
            case FMOD.Studio.EVENT_CALLBACK_TYPE.DESTROYED:
                stringHandle.Free();
                break;
        }
        return FMOD.RESULT.OK;
    }
}
