using UnityEngine;

namespace Scripts.Config
{
    [CreateAssetMenu(fileName = "BalanceConfig", menuName = "ScriptableObjects/BalanceConfig", order = 51)]
    public class BalanceConfig : ScriptableObject
    {
        [field: SerializeField] public float BaseCurrencyPerTap { get; private set; }
        [field: SerializeField] public int EnergyPerTap { get; private set; }
        [field: SerializeField] public int CurrentEnergy { get; private set; }
        [field: SerializeField] public int MaxEnergy { get; private set; }
        [field: SerializeField] public int CollectTimeStep { get; private set; }
        [field: SerializeField] public int CurrencyPerAutoCollect { get; private set; }
        [field: SerializeField] public float Modifier { get; private set; }
        [field: SerializeField] public int IncomeTime { get; private set; }
        [field: SerializeField] public float Divider { get; private set; }
    }
}