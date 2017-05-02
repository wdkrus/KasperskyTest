using System;
using System.Collections.Generic;
using System.Text;

namespace Test2
{
    struct NumberPair : IEquatable<NumberPair>
    {
        public int A { get; set; }
        public int B { get; set; }

        public bool Equals(NumberPair other)
        {
            return (this.A == other.A && this.B == other.B) || 
                   (this.A == other.B && this.B == other.A);
        }

        public override string ToString()
        {
            return String.Format("{0} + {1} = {2}", this.A, this.B, this.A + this.B);
        }
    }
}
