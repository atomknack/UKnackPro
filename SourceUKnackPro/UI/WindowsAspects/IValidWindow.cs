using System;

namespace UKnack.UI.Windows
{
    [System.Obsolete("WIP")]
    public interface IValidWindow
    {
        public void InitWindow(Span<Type> aspects);
        public TAspect GetAspect<TAspect>();
        internal void ValidateRequirements(Span<Type> aspects);
    }
}