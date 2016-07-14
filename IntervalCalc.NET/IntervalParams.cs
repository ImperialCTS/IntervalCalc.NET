using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IntervalCalc
{
    /// <summary>
    /// Contains a list of interval parameters for an expression tree
    /// </summary>
    public class IntervalParams : IEnumerable<IntervalParams.Param>
    {
        public IntervalParams()
        {
            Items = new List<Param>();
            ItemsDic = new Dictionary<Interval, Param>();
        }

        public class Param
        {
            internal Param(int Index, Interval Int) { this.Index = Index; this.Value = Int;  }
            public int Index { get; private set; }
            public Interval Value { get; private set; }
            public double CurrentValue { get; set; }
        }

        List<Param> Items;
        Dictionary<Interval, Param> ItemsDic;

        public Param AddParam(Interval Int)
        {
            var P = new Param(Items.Count, Int);
            Items.Add(P);
            ItemsDic.Add(Int, P);
            return P;
        }

        public IEnumerator<Param> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public Param this[int index]
        {
            get { return Items[index]; }
        }

        public Param this[Interval inv]
        {
            get { return ItemsDic.ContainsKey(inv) ? ItemsDic[inv] : null; }
        }

    }
}
