using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.Assertions;
using Debug = UnityEngine.Debug;
using Random = System.Random;

namespace RandomUtils
{
    [DebuggerDisplay("{From} | {To}")]
    [Serializable]
    public class Range
    {
        public Range(float from, float to)
        {
            From = from;
            To = to;
        }
        public float From;
        public float To;

        public bool IsIn(float val)
        {
            if (val < From)
                return false;
            if (val > To)
                return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}",From,To);
        }
    }

    public class PseudoRandom : Random
    {
        public PseudoRandom(Int32 seed) : base(seed)
        {
        }

        // unity clone: Returns a random float number between and min[inclusive] and max[inclusive]
        public float Range(float min, float max)
        {
            return (max - min) * Value() + min;
        }

        // unity clone: Returns a random integer number between min[inclusive] and max[exclusive]
        public int Range(int min, int max)
        {
            return Next(max - min) + min;
        }

        // unity clone: value
        public float Value()
        {
            return (float) Sample();
        }
    }

    public class RandomHelper
    {
        public delegate void SimpleFunction();
        PseudoRandom _rnd;

        public RandomHelper(int seed)
        {
            _rnd = new PseudoRandom(seed);
        }

        public float Range(float min, float max)
        {
            return _rnd.Range(min, max);
        }

        public int Range(int min, int max)
        {
            return _rnd.Range(min, max);
        }

        public int Int()
        {
            return _rnd.Next();
        }

        public T FromArray<T>(T[] arr)
        {
            return arr[_rnd.Range(0, arr.Length)];
        }
        
        public T[] FromArray<T>(T[] arr, int amount) // get amount values from array
        {
            var src = arr.ToList();
            var res = new T[amount];
            for (int i = 0; i < amount; ++i)
            {
                res[i] = FromList(src);
                src.Remove(res[i]);
            }
            return res;
        }

        public T FromList<T>(List<T> lst)
        {
            return lst[_rnd.Range(0, lst.Count)];
        }

        public List<T> FromList<T>(List<T> lst, int amount)
        {
            var src = new List<T>(lst);
            var res = new List<T>(amount);
            for (int i = 0; i < amount; ++i)
            {
                T tmp = FromList(src);
                res.Add( tmp );
                src.Remove( tmp );
            }
            return res;
        }

        public T FromEnumerable<T>(IEnumerable<T> enumerable)
        {
            Assert.IsTrue(enumerable.Any());
            int index = _rnd.Range(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }

        public float FromRange(Range range)
        {
            return _rnd.Range(range.From, range.To);
        }

        public int FromEnum(Type enumType)
        {
            Array arr = Enum.GetValues(enumType);
            return (int) arr.GetValue(_rnd.Range(0, arr.Length));
        }

        public bool TrySpawnEvent(float probability, SimpleFunction eventFunc = null)
        {
            Assert.IsTrue(probability >= 0.0f);
            Assert.IsTrue(probability <= 1.0f);
            if (_rnd.Value() <= probability)
            {
                if(eventFunc!=null)
                    eventFunc();
                return true;
            }
            return false;
        }

        public int SpawnEvent(float[] probs)
        {
            // get prob line
            float sum = 0;
            for (int i = 0; i < probs.Length; ++i)
                sum += probs[i];

            // select val
            float point = _rnd.Value() * sum;

            // return event
            for (int i = 0; i < probs.Length; ++i)
                if ((point -= probs[i]) < 0)
                    return i;

            return -1;
        }
        
        public bool YesNo(SimpleFunction function = null)
        {
            if (_rnd.Value() < 0.5f)
            {
                if(function!=null)
                    function();
                return true;
            }
            return false;
        }
    }
}
