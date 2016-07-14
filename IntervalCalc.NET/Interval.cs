using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntervalCalc
{
    public class Interval
    {
        public Interval(double A, double B)
        {
            this.A = A;
            this.B = B;
        }

        public double A { get; private set; }
        public double B { get; private set; }

        public double Range => B - A;

        public double Middle => (A + B) / 2;

        public double Any
        {
            get
            {
                throw new Exception("This property is provided as a syntactic sugar and should not be invoked directly.");
            }
        }

        public override string ToString()
        {
            return $"[{A.ToString("0.###")},{B.ToString("0.###")}]";
        }
    }
}
