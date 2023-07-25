// replaced by SetActiveForGameObjects with combination of SOEventToUnityEventAdapter

/*
using System.Collections;
using UKnack.Attributes;

namespace UKnackPro.Assorted;

public abstract class AbstractEnableDisableGameobjectsOn : MonoBehaviour
{
    [SerializeField]
    [ValidReference(nameof(ValidateGameobjects), typeof(GameObject))]
    protected GameObject[] _objectsToEnableDisable;

    [SerializeField]
    protected bool _useInversedValue = false;

    protected void SetActiveGameobjectsBasedOnValue(bool value)
    {
        if (_useInversedValue)
            value = !value;
        foreach (var obj in _objectsToEnableDisable)
        {
            if (obj != null)
                obj.SetActive(value);
        }
    }

    public static void ValidateGameobjects(object gameObjects)
    {
        if (gameObjects == null)
            throw new System.ArgumentNullException(nameof(gameObjects));
        if (gameObjects is IList objectsArray)
        {
            for (var i = 0; i < objectsArray.Count; i++)
            {
                if (objectsArray[i] == null)
                    throw new System.ArgumentNullException($"{i} gameobject is null, nulls not allowed");
                if (objectsArray[i] is not GameObject gameObject)
                    throw new System.ArgumentNullException();
            }
        }
        //else throw new System.Exception($"not IList {((GameObject)gameObjects).name}");

    }
}
*/