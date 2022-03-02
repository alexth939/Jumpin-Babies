// Summary:
//   This technique was developed to deal with
//   MonoBehaviour incapsulation issue. (in my opinion)
//
// Issue:
//   When working with MonoBehaviour objects,
//   you usually want to manipulate attached, to same GameObject, components.
//   If you plan to interact with some object remotely,
//   you have two options:
//        A. Making public components' fields
//        B. Making public methods
//   Then access them via instance.
//   In both cases those public members are exposed to
//   everyone who can GetComponent or FindObject of th instance on scene.

// Special method for gaining access to objects' incapsulated, by interface, methods.
public interface IBindable<ObjectInterface>
{
     ObjectInterface Bind();
}

// In case you need to interact with sender.
public interface IBinder<ObjectInterface, SenderInterface>
{
     ObjectInterface Bind(SenderInterface sender);
}
