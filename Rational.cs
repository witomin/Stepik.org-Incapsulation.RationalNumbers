using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        private int numerator;
        private int denominator;

        public bool IsNan { get { return denominator == 0; } }
        public int Numerator { get { return numerator; } }
        public int Denominator { get { return denominator; } }

        public Rational(int numerator, int denominator)
        {
            if (denominator < 0) numerator = -numerator;
            var NOD = GetNOD(numerator, denominator);
            this.numerator = NOD > 0 ? numerator / NOD : numerator;
            this.denominator = NOD > 0 ? Math.Abs(denominator) / NOD : Math.Abs(denominator);
        }
        public Rational(int numerator) : this(numerator, 1)
        {

        }
        public static Rational operator +(Rational x, Rational y)
        {
            if (x.Denominator == 0 || y.Denominator == 0) return new Rational(0, 0);
            var CommonDenominator = GetCommonDenominator(x.Denominator, y.Denominator);
            var Numerator = x.Numerator * (CommonDenominator / x.Denominator) + y.Numerator * (CommonDenominator / y.Denominator);
            return new Rational(Numerator, CommonDenominator);
        }
        public static Rational operator -(Rational x, Rational y)
        {
            if (x.Denominator == 0 || y.Denominator == 0) return new Rational(0, 0);
            var CommonDenominator = GetCommonDenominator(x.Denominator, y.Denominator);
            var Numerator = x.Numerator * (CommonDenominator / x.Denominator) - y.Numerator * (CommonDenominator / y.Denominator);
            return new Rational(Numerator, CommonDenominator);
        }
        public static Rational operator *(Rational x, Rational y)
        {
            return new Rational(x.numerator * y.numerator, x.denominator * y.denominator);
        }
        public static Rational operator /(Rational x, Rational y)
        {
            if (x.Denominator == 0 || y.Denominator == 0) return new Rational(0, 0);
            var divider = new Rational(y.denominator, y.numerator);
            return x * divider;
        }
        public static implicit operator double(Rational x)
        {
            if (x.IsNan) return double.NaN;
            double result = (double)x.numerator / (double)x.denominator;
            return result;
        }
        public static explicit operator int(Rational Rational)
        {
            if (Rational.IsNan) throw new SystemException();
            if (Rational.numerator % Rational.denominator != 0) throw new SystemException();
            return Rational.numerator / Rational.denominator;
        }
        public static implicit operator Rational(int x)
        {
            return new Rational(x);
        }
        private static int GetNOD(int val1, int val2)
        {
            val1 = Math.Abs(val1);
            val2 = Math.Abs(val2);
            while ((val1 != 0) && (val2 != 0))
            {
                if (val1 > val2)
                    val1 -= val2;
                else
                    val2 -= val1;
            }

            return Math.Max(val1, val2);
        }
        private static int GetCommonDenominator(int y1, int y2)
        {
            int y3;
            if ((y2 >= y1) && (y2 % y1 == 0))
            {
                y3 = y2;
                return y3;
            }
            else if ((y1 > y2) && (y1 % y2 == 0))
            {
                y3 = y1;
                return y3;
            }
            else

                y3 = y2 * y1;
            return y3;
        }
    }
}
