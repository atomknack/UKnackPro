using System.Collections.Generic;
using System.Linq;


namespace UKnack.UI
{
    internal interface IOnScreenOrder
    {
        private static List<IOnScreenOrder> s_allProviders = new List<IOnScreenOrder>();
        /// <summary>
        /// UIDocument sorting order, bigger it is - the closer it to camera (unlike normal 3d objects).
        /// Document with biggest sorting order is on top of all others
        /// </summary>
        public float SortingOrder { get; }

        public static void RegisterActive(IOnScreenOrder provider) => s_allProviders.Add(provider);
        public static void UnRegisterInactive(IOnScreenOrder provider) => s_allProviders.Remove(provider);

        public static bool TryGetMaxZInversedOrder(out float order)
        {
            if (s_allProviders.Count > 0)
            {
                order = s_allProviders.Max(x => x.SortingOrder);
                return true;
            }
            order = 0f;
            return false;
        }
        public static float NextFreeSpotOnTop()
        {
            const float STEP = 100f;
            if (TryGetMaxZInversedOrder(out float order))
                return order + STEP;
            return 10000f;
        }
    }
}
