using System;

namespace Deadspell
{
    public readonly struct Rational
    {
        public long Numerator { get; }
        public long Denominator { get; }

        public static Rational NaN => new Rational(0, 0);
        public static Rational Zero => new Rational(0, 1);
        
        public Rational(long numerator, long denominator)
        {
            Numerator = numerator;
            Denominator = denominator;

            if (Denominator < 0)
            {
                Numerator *= -1;
                Denominator *= -1;
            }

            if (Denominator == 0)
            {
                Numerator = 0;
                return;
            }

            if (Numerator == 0)
            {
                Denominator = 1;
                return;
            }
        }

        public Rational(int value)
        {
            Numerator = value;
            Denominator = 1;
        }

        private static long GetGCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a == 0 ? b : a;
        }

        private static long GetLCD(long a, long b)
        {
            return (a * b) / GetGCD(a, b);
        }

        public Rational Canonical
        {
            get
            {
                if (Denominator == 0)
                {
                    return NaN;
                }
                Rational modified = this;

                long gcd;
                while (Math.Abs(gcd = GetGCD(modified.Numerator, modified.Denominator)) != 1)
                {
                    modified = new Rational(modified.Numerator / gcd, modified.Denominator / gcd);
                }

                return modified;
            }
        }

        public static implicit operator Rational(int integer)
        {
            return new Rational(integer);
        }

        public static explicit operator double(Rational rational)
        {
            return (double)rational.Numerator / (double)rational.Denominator;
        }

        public Rational ToDenominator(long target)
        {
            if (target < Denominator)
            {
                return this;
            }

            if (target % Denominator != 0)
            {
                return this;
            }

            if (Denominator != target)
            {
                long factor = target / Denominator;
                return new Rational(Numerator * factor, target);
            }

            return this;
        }

        public static Rational operator +(Rational lhs, Rational rhs)
        {
            if (lhs == NaN)
            {
                return rhs;
            }
            if (rhs == NaN)
            {
                return lhs;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return new Rational(f1.Numerator + f2.Numerator, lcd);
        }
        public static Rational operator -(Rational lhs, Rational rhs)
        {
            if (lhs == NaN)
            {
                return rhs;
            }
            else if (rhs == NaN)
            {
                return lhs;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return new Rational(f1.Numerator - f2.Numerator, lcd);
        }
        
        public static Rational operator *(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.Numerator * rhs.Numerator, lhs.Denominator * rhs.Denominator);
        }
        public static Rational operator /(Rational lhs, Rational rhs)
        {
            return new Rational(lhs.Numerator * rhs.Denominator, lhs.Denominator * rhs.Numerator);
        }
        
        
        public static bool operator ==(Rational lhs, Rational rhs)
        {
            var f1 = lhs.Canonical;
            var f2 = rhs.Canonical;
            return (f1.Numerator == f2.Numerator) && (f1.Denominator == f2.Denominator);
        }
        public static bool operator !=(Rational lhs, Rational rhs)
        {
            return !(lhs == rhs);
        }

        public static bool operator >(Rational lhs, Rational rhs)
        {
            if (lhs == NaN || rhs == NaN)
            {
                return false;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return f1.Numerator > f2.Numerator;
        }

        public static bool operator >=(Rational lhs, Rational rhs)
        {
            if (lhs == NaN || rhs == NaN)
            {
                return false;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return f1.Numerator >= f2.Numerator;
        }

        

        public static bool operator <(Rational lhs, Rational rhs)
        {
            if (lhs == NaN || rhs == NaN)
            {
                return false;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return f1.Numerator < f2.Numerator;
        }
        
        public static bool operator <=(Rational lhs, Rational rhs)
        {
            if (lhs == NaN || rhs == NaN)
            {
                return false;
            }

            long lcd = GetLCD(lhs.Denominator, rhs.Denominator);
            var f1 = lhs.ToDenominator(lcd);
            var f2 = rhs.ToDenominator(lcd);
            return f1.Numerator <= f2.Numerator;
        }
        
        public long FloorToInt()
        {
            if (this < Zero)
            {
                return (Numerator - Denominator + 1) / Denominator;
            }
            return Numerator / Denominator;
        }
        
        public long CeilToInt()
        {
            if (this < Zero)
            {
                return Numerator / Denominator;
            }
            return (Numerator + Denominator - 1) / Denominator;
        }

        public override string ToString()
        {
            return $"{Numerator}/{Denominator}";
        }

        public override int GetHashCode()
        {
            return (int)(Numerator ^ Denominator);
        }

        public override bool Equals(object obj)
        {
            return obj is Rational r && r == this;
        }
    }
}