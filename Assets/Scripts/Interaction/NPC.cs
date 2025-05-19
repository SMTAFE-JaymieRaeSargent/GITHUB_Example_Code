using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    private string toolTip = "";

    private void Start()
    {
        toolTip = $"Press {KeybindManager.keys["Interact"].ToString()} to talk";
    }
    public void OnInteraction()
    {
        Debug.Log("Talk to NPC");
    }

    public string ToolTip()
    {
        return toolTip;
    }
}
