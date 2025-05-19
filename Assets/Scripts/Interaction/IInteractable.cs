//public interface IInteractable: This defines an interface called IInteractable.
public interface IInteractable
{
    //void OnInteraction(): This is a method that any class implementing IInteractable must define.
    void OnInteraction();
    string ToolTip();
}
#region What is an Interface in C#?
/*
    
    An interface in C# defines a contract for classes. It specifies what methods or properties 
    a class must implement but doesn’t dictate how they should be implemented. This is useful 
    for creating a common structure that multiple classes can follow, allowing for more flexible 
    and modular code.

 */
#endregion
#region Key Points
/*
 * 
    - An interface only defines method signatures and properties.
    - A class that implements an interface must provide concrete definitions for all of the interface’s methods.
    - Interfaces allow different classes to be treated the same way if they share the same interface.

*/
#endregion
#region How to Implement
/*
    When a class implements an interface, it must provide the implementation of all the methods defined in the interface.
    In this case, the Door class implements the IInteractable interface and provides the specific behaviour for the OnInteraction() method.

public class Door : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
        Debug.Log("The door opens!");
    }
}

    You can have multiple classes implementing the same interface, each providing its own version of the method.

public class Chest : MonoBehaviour, IInteractable
{
    public void OnInteraction()
    {
       Debug.Log("The chest opens and reveals treasure!");
    }
}
    Once classes implement the interface, you can interact with them through the interface type, allowing you to treat different objects in a unified way.
 */
#endregion
#region Why Interfaces
/*
    Code Flexibility: By programming to interfaces, you can write flexible code that works with any class implementing the interface, without knowing the specific type of class.
    Multiple Implementations: A class can implement multiple interfaces, making it versatile.
    Decoupling: It helps decouple code by separating the "what" (interface) from the "how" (class implementation).
*/
#endregion