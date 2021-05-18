using UnityEngine;

[System.Serializable]
public class DescriptionHolder
{
    [SerializeField] 
    private string _title;

    [SerializeField] 
    private string _type;

    [SerializeField][TextArea]
    private string _descriptionOffensive;

    [SerializeField][TextArea]
    private string _descriptionDefensive;

    public string Title { get => _title; }

    public string Type { get => _type; }

    public string DescriptionOffensive { get => _descriptionOffensive; }

    public string DescriptionDefensive { get => _descriptionDefensive; }
}
