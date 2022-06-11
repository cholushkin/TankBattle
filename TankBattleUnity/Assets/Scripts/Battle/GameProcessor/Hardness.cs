using System;
using UnityEngine;

namespace Battle
{
    [Serializable]
    public class Hardness
    {
        public float MobCount;
        public float ProbabilityEasyMob;
        public float ProbabilityMedMob;
        public float ProbabilityHardMob;

        public static Hardness Lerp(Hardness a, Hardness b, float t)
        {
            var res = new Hardness();

            res.MobCount = Mathf.RoundToInt(Mathf.Lerp(a.MobCount, b.MobCount, t));
            res.ProbabilityEasyMob = Mathf.Lerp(a.ProbabilityEasyMob, b.ProbabilityEasyMob, t);
            res.ProbabilityMedMob = Mathf.Lerp(a.ProbabilityMedMob, b.ProbabilityMedMob, t);
            res.ProbabilityHardMob = Mathf.Lerp(a.ProbabilityHardMob, b.ProbabilityHardMob, t);

            return res;
        }
    }
}
