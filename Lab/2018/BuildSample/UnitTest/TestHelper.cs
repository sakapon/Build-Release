using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    public static class TestHelper
    {
        public static Action<T, TResult> CreateAssertion<T, TResult>(Func<T, TResult> target) =>
            (arg, expected) => Assert.AreEqual(expected, target(arg));

        public static Action<T1, T2, TResult> CreateAssertion<T1, T2, TResult>(Func<T1, T2, TResult> target) =>
            (arg1, arg2, expected) => Assert.AreEqual(expected, target(arg1, arg2));

        public static Action<T1, T2, T3, TResult> CreateAssertion<T1, T2, T3, TResult>(Func<T1, T2, T3, TResult> target) =>
            (arg1, arg2, arg3, expected) => Assert.AreEqual(expected, target(arg1, arg2, arg3));
    }
}
