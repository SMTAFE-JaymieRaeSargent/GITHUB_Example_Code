using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private string toolTip = "";
    [SerializeField] string[] _lines;
    [SerializeField] string _name;
    [SerializeField] Sprite _face;
    private void Start()
    {
        toolTip = $"Press {KeybindManager.keys["Interact"].ToString()} to talk";
    }
    public void OnInteraction()
    {
       // Debug.Log("Talk to NPC");
        //Run behaviour from Dialogue Manager instance 
        DialogueManager.instance.OnActive(_lines, _name, _face);
    }

    public string ToolTip()
    {
        return toolTip;
    }
}
