using System;
using UnityEngine;


namespace Battle
{
    public enum UnitType
    {
        Unknown,
        Tank,
        IntruderBase,
        IntruderMed,
        IntruderHard
    }

    public enum UnitState
    {
        Oblivion,
        Dead,
        Respawning,
        Active
    }

    [Serializable]
    public class UnitSettings
    {
        public float Health;
        [Range(0,1)]
        public float Armour;
        public float Dangerous; // for now it is damage of unit ( but in future damage will be determined by weapon)
    }

    public interface IUnit
    {
        UnitType GetUnitType();
        UnitState GetUnitState();
        void GotoState(UnitState state);
    }
}
