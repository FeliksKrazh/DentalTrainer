using UnityEngine;

namespace DentalTrainer_FeliksKrazhau
{
    [CreateAssetMenu(fileName = "WorldToScreenConvertSetting.asset", menuName = "Costom/WorldToScreenConvertSetting")]
    public class WorldToScreenConvertSetting : ScriptableObject
    {
        [SerializeField] private float distanceView = 50.0f;
        [SerializeField] private float angleView = 15.0f;

        public float DistanceView
        {
            get => distanceView;
        }
        public float AngleView
        {
            get => angleView;
        }
    }
}
