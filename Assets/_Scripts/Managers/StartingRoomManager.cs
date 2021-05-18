using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

///
/// Author: Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Logic of the tutorial area and dialog system trigger
/// ==============================================
/// Changelog:
/// Samuel Müller: added player Spawn
/// ==============================================
///

public class StartingRoomManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _playerSpawnPoint;
    [SerializeField] private GameObject timothe;
    [SerializeField] private List<GameObject> dummys;
    [SerializeField] private GameObject portal;
    [SerializeField] private GameObject[] spawnPoints;

    private bool estellaAppeared = false;
    private bool timotheAppeared = false;

    private bool firstDummyDestroyed = false;
    private bool isDummyDialogOver = false;
    
    
    private bool defenseWasUsed = false;

    //Sound Dialog
    private SoundContainer _soundContainer;
    private AudioSource _soundSource;
    private int dialogCounter;
    private LanguageManager _languageManager;
    private bool isGerman;
    
    //UI Dialog
    private PopUpManager _hudCanvas;
    
    [FMODUnity.EventRef]
    public string fmodEstellaTalkEvent, fmodTimTalkEvent;

    FMOD.Studio.EVENT_CALLBACK dialogueCallback;
    
    // Start is called before the first frame update
    void Start()
    {
        //portal.SetActive(false);
        _soundContainer = SoundContainer.Instance;
        _soundSource = _soundContainer.GetComponent<AudioSource>();
        _languageManager = LanguageManager.Instance;
        _hudCanvas = GameObject.FindWithTag("HUD").GetComponent<PopUpManager>();

        dialogueCallback = DialogueEventCallback;
       Invoke("EstellaAppears", 2);
    }

    void OnEnable()
    {
        EnemyStatHandler.DummyWasDestroyed += FirstDummyWasDestroyed;
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
    
    void PlayDialogue(string key, soundRole role)
    {
        FMOD.Studio.EventInstance dialogueInstance;
        switch (role)
        {
            case soundRole.Estella:
                dialogueInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEstellaTalkEvent);
                dialogueInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(player, player.GetComponent<Rigidbody>()));
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
    
    private void Update()
    {
        if(timotheAppeared && estellaAppeared && firstDummyDestroyed && !defenseWasUsed)
        {
            if (Input.GetButtonDown("Defense"))
            {
                DefenseWasUsed();
            }
        }

        if (Input.GetButtonDown("Interact"))
        {
            portal.SetActive(true);
        }
    }

    //Delegate event that is thrown, when the SoundContainer is empty again
    public void VoicelineHasEnded()
    {
        getLanguage();
        if (!defenseWasUsed)
        {
            if (timotheAppeared && estellaAppeared && firstDummyDestroyed)
            {
                dialogCounter++;
                //Invoke("FirstDummyWasDestroyed", 2);
                if (dialogCounter < _soundContainer.soundScenes[0].soundEvents[2].soundLines.Count)
                {
                    if (isGerman)
                    {
                        _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);
                    }
                    else
                    {
                        _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);
                    }
                    
                    PlayDialogue(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].lineID,_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);
                    Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].lineAudio.length + _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].waitTimeInMS / 1000);
                }
                else
                {
                    _hudCanvas.DisableDialog();
                    isDummyDialogOver = true;
                }
                //Invoke("DefenseWasUsed", 2);
                return;
            }
            else if (timotheAppeared && estellaAppeared)
            {
                dialogCounter++;
                //Invoke("FirstDummyWasDestroyed", 2);
                if (dialogCounter < _soundContainer.soundScenes[0].soundEvents[1].soundLines.Count)
                {
                    if (isGerman)
                    {
                        _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
                    }
                    else
                    {
                        _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
                    }
                    
                    
                    PlayDialogue(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].lineID, _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
                    Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].lineAudio.length + _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].waitTimeInMS / 1000);
                }
                else
                {
                    _hudCanvas.DisableDialog();
                    spawnDummies();
                }
                return;
            }
            else if (estellaAppeared)
            {
                _hudCanvas.DisableDialog();
                Invoke("TimotheAppears", 2);
                timothe.GetComponent<TimothyMovementTrigger>().TriggerMovement();
                return;
            }
        }
        else
        {
            dialogCounter++;
            //Invoke("FirstDummyWasDestroyed", 2);
            if (dialogCounter < _soundContainer.soundScenes[0].soundEvents[3].soundLines.Count)
            {
                if (isGerman)
                {
                    _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
                }
                else
                {
                    _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
                }
                
                PlayDialogue(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].lineID,_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
                Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].lineAudio.length + _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].waitTimeInMS / 1000);
            }
            else
            {
                _hudCanvas.DisableDialog();
                portal.SetActive(true);
            }
        }
        
    }

    private void EstellaAppears()
    {
        Debug.Log("estella just arrived");
        
        getLanguage();

        estellaAppeared = true;

        if (isGerman)
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[0].soundLines[0].subtileGer, _soundContainer.soundScenes[0].soundEvents[0].soundLines[0].role);
        }
        else
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[0].soundLines[0].subtileEng, _soundContainer.soundScenes[0].soundEvents[0].soundLines[0].role);
        }
        
        PlayDialogue(_soundContainer.soundScenes[0].soundEvents[0].soundLines[dialogCounter].lineID, _soundContainer.soundScenes[0].soundEvents[0].soundLines[dialogCounter].role);
        
        _hudCanvas.EnableDialog();
        
        Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[0].soundLines[0].lineAudio.length);
    }

    private void TimotheAppears()
    {
        getLanguage();
        Debug.Log("timothe just arrived");
        timotheAppeared = true;
        dialogCounter = 0;

        if (isGerman)
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
        }
        else
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
        }
        
        
        PlayDialogue(_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].lineID,_soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].role);
        
        _hudCanvas.EnableDialog();
        Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[1].soundLines[dialogCounter].lineAudio.length + 1);

    }

    private void spawnDummies()
    {
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            Instantiate(dummys[i], spawnPoints[i].transform);
        }
    }

    private void FirstDummyWasDestroyed()
    {

        getLanguage();
        Debug.Log("dummy was destoryed");
        firstDummyDestroyed = true;
        //trigger Voiceline
        dialogCounter = 0;

        if (isGerman)
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);
        }
        else
        {
            _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);
        }

        PlayDialogue(_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].lineID,_soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].role);

        _hudCanvas.EnableDialog();
        Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[2].soundLines[dialogCounter].lineAudio.length + 1);    
        EnemyStatHandler.DummyWasDestroyed -= FirstDummyWasDestroyed;

    }

    private void DefenseWasUsed()
    {
        getLanguage();
        if (isDummyDialogOver)
        {
            Debug.Log("defence was used");

            defenseWasUsed = true;
            //trigger Voiceline
            dialogCounter = 0;
        
            if (isGerman)
            {
                _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].subtileGer, _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
            }
            else
            {
                _hudCanvas.setDialog(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].subtileEng, _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
            }
        
            PlayDialogue(_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].lineID,_soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].role);
        
            _hudCanvas.EnableDialog();
            Invoke("VoicelineHasEnded", _soundContainer.soundScenes[0].soundEvents[3].soundLines[dialogCounter].lineAudio.length + 1);
        }
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
}
