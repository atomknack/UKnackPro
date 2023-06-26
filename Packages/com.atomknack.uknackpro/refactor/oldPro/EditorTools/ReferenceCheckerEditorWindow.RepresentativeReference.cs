#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using UKnack.Common;

namespace UKnack.EditorTools
{

    public partial class ReferenceCheckerEditorWindow
    {
        public readonly struct RepresentativeReference
        {
            public readonly UnityEngine.Object obj;
            public readonly string referencePath;
            public readonly string[] wasReferencedBy;

            public static RepresentativeReference[] CreateArray(List<FoundReference> references)
            {
                List<RepresentativeReference> result = new List<RepresentativeReference>();
                List<string> wasRepresentedBy = new List<string>();
                string prev = null;
                UnityEngine.Object obj = null;
                foreach (var ordered in references.OrderBy(x => x.referenceStringRepresentation))
                {
                    var newPath = ordered.referenceStringRepresentation;
                    if (newPath != prev)
                    {
                        AddToResultsIfCanAndFlushTempList(result, obj, prev, wasRepresentedBy);
                        prev = newPath;
                        obj = ordered.reference;
                    }
                    SortedReferences.AddIfNotExists(wasRepresentedBy, CommonStatic.GetFullPath_Recursive(ordered.wasReferencecBy));//wasRepresentedBy.Add(ReferenceFinder.GetFullPath(ordered.wasReferencecBy));
                }
                AddToResultsIfCanAndFlushTempList(result, obj, prev, wasRepresentedBy);
                return result.ToArray();
            }

            private static void AddToResultsIfCanAndFlushTempList(List<RepresentativeReference> results, UnityEngine.Object obj, string referencePath, List<string> tempWasReferencedBy)
            {
                if (tempWasReferencedBy == null)
                    throw new ArgumentNullException(nameof(tempWasReferencedBy));
                if (referencePath == null)
                {
                    if (tempWasReferencedBy.Count > 0)
                        throw new ArgumentException($"reference is null, but it was referenced by {tempWasReferencedBy.Count} items: {string.Join(',', tempWasReferencedBy)}");
                    return;
                }
                results.Add(new RepresentativeReference(obj, referencePath, tempWasReferencedBy));
                tempWasReferencedBy.Clear();
            }

            public RepresentativeReference(UnityEngine.Object obj, string referencePath, List<string> wasReferencedBy)
            {
                this.obj = obj;
                this.referencePath = referencePath;
                this.wasReferencedBy = wasReferencedBy.ToArray();
            }
        }
    }

}
#endif