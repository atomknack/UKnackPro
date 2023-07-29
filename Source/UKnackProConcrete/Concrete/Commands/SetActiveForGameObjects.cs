using System;
using System.Collections;
using UnityEngine;
using UKnack.Attributes;
using UKnack.Commands;

namespace UKnack.Concrete.Commands
{
    [Obsolete("Not tested")]
    [AddComponentMenu("UKnack/Commands/SetActiveForGameObjects")]
    // For sinle gameobject SetActive can be called just from UnityEvent, so no need for separate script
    internal class SetActiveForGameObjects : CommandMonoBehaviour<bool>
    {
        [SerializeField]
        [ValidReference(nameof(ValidateGameobjects), typeof(GameObject))]
        protected GameObject[] _objectsToSetActive;

        public void SetActiveAll() =>
            SetActiveGameobjectsBasedOnValue(_objectsToSetActive, true);

        public void SetInactiveAll() =>
            SetActiveGameobjectsBasedOnValue(_objectsToSetActive, false);

        public override void Execute(bool t) =>
            SetActiveGameobjectsBasedOnValue(_objectsToSetActive, t);

        private void OnEnable()
        {
            ValidateGameobjects(_objectsToSetActive);
        }

        public static void SetActiveGameobjectsBasedOnValue(ReadOnlySpan<GameObject> gos, bool value)
        {
            foreach (var obj in gos)
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
        }
    }
}
