using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
///
/// Author: Samuel Müller: sm184, Marvin Kalchschmidt: mk306
/// Description: Contains Name, Type, Descriptions, Stats and defines Sprites and Colors for Elements, Wildcards and Weapons
/// ==============================================
/// Changelog: 
/// ==============================================
///
[CreateAssetMenu(fileName = "new AbilityHUDElement", menuName = "HUD/AbilityHUDElement")]
public class AbilityHUDElement : ScriptableObject
{
    [SerializeField]
    private string _name = "name";

    [SerializeField]
    private DescriptionHolder _englishDescription;

    [SerializeField]
    private DescriptionHolder _germanDescription;

    [SerializeField] private Sprite _sprite;

    [SerializeField] private Sprite _background;

    [SerializeField] private Color _color;

    public string Name { get => _name; }

    public Sprite Sprite { get => _sprite;  }

    public DescriptionHolder Description
    {
        get
        {
            switch (LanguageManager.Instance.Language)
            {
                case LanguageEnum.German: return _germanDescription;
                default: return _englishDescription;
            }
        }
    }

    public Sprite Background { get => _background; }

    public Color Color { get => _color; }
}