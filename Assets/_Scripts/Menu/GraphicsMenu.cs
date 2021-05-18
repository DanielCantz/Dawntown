///
/// Author: Christopher Beck
/// Description: This class defines functions for the options in the main and pause menu, including changing the quality settings
/// ==============================================
/// Changelog:
/// 06/29/2020 - Christopher Beck - Created class
/// ==============================================
///
using UnityEngine;

public class GraphicsMenu : MonoBehaviour
{
    /// <summary>
    /// Array of quality options
    /// </summary>
    string[] _names;

    private void Start()
    {
            _names = QualitySettings.names;
    }

    /// <summary>
    /// This is currently used in the main menu
    /// </summary>
    /// <param name="quality"></param>
    public void SetGraphicQuality(string quality)
    {
        QualitySettings.SetQualityLevel(System.Array.IndexOf(_names, quality), true);
    }
}
