#if UNITY_EDITOR
using UnityEngine;

namespace UKnack.EditorTools
{

    public partial class ReferenceCheckerEditorWindow
    {
        public readonly struct FoundReference
        {
            public readonly UnityEngine.Object reference;
            public readonly string referenceStringRepresentation;
            public readonly UnityEngine.GameObject wasReferencecBy;

            public FoundReference(UnityEngine.Object reference, string referenceStringRepresentation, GameObject wasReferencecBy)
            {
                this.reference = reference;
                this.referenceStringRepresentation = referenceStringRepresentation;
                this.wasReferencecBy = wasReferencecBy;
            }
        }
    }
}
#endif