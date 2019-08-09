using System;
using System.Collections.Generic;

namespace PokerGame.Extensions
{
    public static class StackExtensions
    {
        public static void ForEach<T>(this Stack<T> myStack, Action<T> action)
        {
            foreach(var item in myStack)
            {
                action.Invoke(item);
            }
        }
    }
}
