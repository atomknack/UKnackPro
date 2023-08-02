using UnityEngine;

namespace UKnack.UI;

[DisallowMultipleComponent]
public abstract class LayoutProvider : MonoBehaviour
{
    internal abstract void RegisterScript(ILayoutDependant dependant);

}