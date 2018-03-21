using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackWebAPI.Exceptions
{
    /// <summary>
    /// <para>Exception thrown when any of the following methods cause an error:</para>
    /// <para><see cref="BackpackWrapper.GetCommunityPricesAsync(long)"/></para>
    /// <para><see cref="BackpackWrapper.GetCurrenciesAsync(int)"/></para>
    /// <para><see cref="BackpackWrapper.GetPriceHistoryAsync(string, string, int, bool, int)"/></para>
    /// <para><see cref="BackpackWrapper.GetSpecialItemsAsync(int)"/></para>
    /// <para><see cref="BackpackWrapper.GetUserInfoAsync(params ulong[] steamIds)"/></para>
    /// <para><see cref="BackpackWrapper.SearchClassifiedsAsync(string, bool, Dictionary{string, string}, string, int, int, bool, ulong)"/></para>
    /// </summary>
    public class BackpackRequestException : Exception
    {
        /// <summary>
        /// Basic exception.
        /// </summary>
        public BackpackRequestException()
        {
        }

        /// <summary>
        /// Basic exception with an additional message.
        /// </summary>
        /// <param name="message">A custom message to include.</param>
        public BackpackRequestException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Basic exception with an additional message and inner exception.
        /// </summary>
        /// <param name="message">A custom message to include.</param>
        /// <param name="inner">The inner exception to include.</param>
        public BackpackRequestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
