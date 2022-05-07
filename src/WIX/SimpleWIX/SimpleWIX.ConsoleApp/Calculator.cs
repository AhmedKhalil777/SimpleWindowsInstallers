using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleWIX.ConsoleApp
{
    public static class CalculatorProvider
    {
        public static Func<T, T, T> BuildOperation<T>(string op)
            where T : struct
        {
            return op switch
            {
                "+" => new Func<T,T,T>((T first, T second) => (T)((dynamic)first+ (dynamic)second)),
                "-" => new Func<T, T, T>((T first, T second) => (T)((dynamic)first - (dynamic)second)),
                "*" => new Func<T, T, T>((T first, T second) => (T)((dynamic)first * (dynamic)second)),
                "/" => new Func<T, T, T>((T first, T second) => (T)((dynamic)first / (dynamic)second)),
                "%" => new Func<T, T, T>((T first, T second) => (T)((dynamic)first % (dynamic)second)),
                _ => throw new ArgumentException()
            };
        }
    }
}
