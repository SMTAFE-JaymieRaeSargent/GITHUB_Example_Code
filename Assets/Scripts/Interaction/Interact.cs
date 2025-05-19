using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public Text toolTip;
    void Update()
    {
        // create a ray (a Ray is ?? a beam, line that comes into contact with colliders)
        Ray interactionRay;
        // this ray shoots forward from the center of the camera 
        interactionRay = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        //create hit info (this holds the info for the stuff we interact with)
        RaycastHit hitInfo;
        //if this physics ray that gets cast in a direction hits a object withing our distance and or layer
        if (Physics.Raycast(interactionRay, out hitInfo, 10))
        {
            if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable displayToolTip))
            {
                toolTip.text = displayToolTip.ToolTip();
            }
            //if our interaction button or key is pressed
            if (Input.GetKeyDown(KeybindManager.keys["Interact"]))
            {
                // If the ray hits a collider of an object that has a script subscribed to IInteractable
                if (hitInfo.collider.TryGetComponent<IInteractable>(out IInteractable interact))
                {
                    // Run the OnInteraction function
                    interact.OnInteraction();
                }
            }
        }
        else 
        {
            if (toolTip.text != "")
            {
                toolTip.text = "";
            }
        }
    }
}
