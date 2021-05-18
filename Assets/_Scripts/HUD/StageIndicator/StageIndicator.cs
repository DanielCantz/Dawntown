using UnityEngine;
using UnityEngine.UI;
///
/// Author: Samuel Müller: sm184
/// Description: holds information about the current stage and displays it to the hud.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class StageIndicator : MonoBehaviour
{
    private int _stage = 1;

    private int _level = 1;

    [SerializeField]
    private Text _text;

    public Text Text
    {
        get
        {
            return _text;
        }
        set
        {
            _text = value;
            Refresh();
        }
    }

    private void Update()
    {
        Refresh();
    }

    private void Refresh()
    {
        _text.text = _stage.ToString() + "." + _level.ToString();
    }

    public void IncreaseStage()
    {

        Mathf.Clamp(_level++, 1, 5);
        if (_level > 4)
        {
            _stage++;
            _level = 1;
        }
    }

    public int getStage()
    {
        return _stage;
    }
}