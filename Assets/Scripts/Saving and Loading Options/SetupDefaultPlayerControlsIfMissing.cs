using System;
using UnityEngine;

public class SetupDefaultPlayerControlsIfMissing : MonoBehaviour
{
    void Awake()
    {
        if (KeybindManager.keys.Count <= 0)
        {
            for (int i = 0; i < DefaultControls.keyNames.Length; i++)
            {
                KeybindManager.keys.Add(DefaultControls.keyNames[i], (KeyCode)Enum.Parse(typeof(KeyCode), DefaultControls.keyValues[i]));
            }
        }
    }

}
