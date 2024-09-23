using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

namespace RailShooter.Player
{
    public class PlayerFollow : MonoBehaviour
    {
        [SerializeField] private SplineContainer container;
        [SerializeField] private float speed = 0.04f;

        [SerializeField] private SplinePathData pathData;

        SplinePath path;

        float progressRatio;
        float progress;
        float totalLength;

        private void Start()
        {
            path = new SplinePath(CalculatePath());

            StartCoroutine(FollowCoroutine());
        }

        private List<SplineSlice<Spline>> CalculatePath()
        {
            var localToWorldMatrix = container.transform.localToWorldMatrix;

            //TODO: Remove linq
            var enabledSlices = pathData.slices.Where(slice => slice.isEnabled).ToList();

            var slices = new List<SplineSlice<Spline>>();
            totalLength = 0;

            foreach(var sliceData in enabledSlices)
            {
                var spline = container.Splines[sliceData.splineIndex];
                var slice = new SplineSlice<Spline>(spline, sliceData.splineRange, localToWorldMatrix);

                slices.Add(slice);

                sliceData.distanceFromStart = totalLength;
                sliceData.sliceLenght = slice.GetLength();

                totalLength += sliceData.sliceLenght;
            }

            return slices;
        }

        private IEnumerator FollowCoroutine()
        {
            for(var n = 0; ; ++n)
            {
                progressRatio = 0;
                while(progressRatio <= 1f)
                {
                    var pos = path.EvaluatePosition(progressRatio);
                    var direction = path.EvaluateTangent(progressRatio);

                    transform.position = pos;
                    transform.LookAt(pos + direction);

                    progressRatio += speed * Time.deltaTime;

                    progress = progressRatio * totalLength;
                    yield return null;
                }

                foreach(var sliceData in pathData.slices)
                {
                    sliceData.isEnabled = true;
                }

                path = new SplinePath(CalculatePath());
            }
        }
    }
}