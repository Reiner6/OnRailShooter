using UnityEngine.Splines;

namespace RailShooter.Player
{
    [System.Serializable]
    public class SliceData
    {
        public int splineIndex;
        public SplineRange splineRange;

        public bool isEnabled = true;
        public float sliceLenght;
        public float distanceFromStart;
    }
}
