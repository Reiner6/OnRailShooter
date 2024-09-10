using KBCore.Refs;
using UnityEngine;

namespace RailShooter.UI
{
    public class Reticle : ValidatedMonoBehaviour
    {
        [SerializeField] private Transform targetPoint;
        [SerializeField, Self] private RectTransform rectTransform;

        private void Update() => rectTransform.position = UnityEngine.Camera.main.WorldToScreenPoint(targetPoint.position);        
    }
}
