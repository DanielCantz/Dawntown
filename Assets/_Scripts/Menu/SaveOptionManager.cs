using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

///
/// Author: Samuel Müller: sm184
/// Description: Saves volume changes to make them transcend scenes.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class SaveOptionManager : Singleton<SaveOptionManager>
{
    [SerializeField]  VolumeMenu _volumeMenu;
    [SerializeField] LanguageManager _languageManager;

    private void Start()
    {
        LoadVolumes();
        LoadLanguage();
    }

    public void SaveVolumes()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Option.dat", FileMode.OpenOrCreate);
        try
        {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, _volumeMenu.Volumes);
        }
        catch (SerializationException e){
            Debug.LogError("SerializationExeption: " + typeof(SaveOptionManager).ToString() + " - " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void SaveLanguage()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Language.dat", FileMode.OpenOrCreate);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(file, _languageManager.Language);
        }
        catch (SerializationException e)
        {
            Debug.LogError("SerializationExeption: " + typeof(SaveOptionManager).ToString() + " - " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void LoadVolumes()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Option.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _volumeMenu.Volumes = formatter.Deserialize(file) as VolumeMenu.VolumeStats;
            _volumeMenu.MapSlidersByVolumes();
            _volumeMenu.MapVolumesByValues();
        }
        catch (SerializationException e)
        {
            Debug.LogError("SerializationExeption: " + typeof(SaveOptionManager).ToString() + " - " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }

    public void LoadLanguage()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Language.dat", FileMode.Open);
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            _languageManager.SetLanguage((int)formatter.Deserialize(file));
        }
        catch (SerializationException e)
        {
            Debug.LogError("SerializationExeption: " + typeof(SaveOptionManager).ToString() + " - " + e.Message);
        }
        finally
        {
            file.Close();
        }
    }
}
