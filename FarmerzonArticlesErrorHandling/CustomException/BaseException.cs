using System;
using System.Collections.Generic;

namespace FarmerzonArticlesErrorHandling.CustomException
{
    public class BaseException : Exception
    {
        public IList<string> Messages = new List<string>();
        
        public BaseException()
        {
            // nothing to do here
        }

        public BaseException(string message) : base(message)
        {
            Messages.Add(message);
        }

        public BaseException(string message, Exception inner) : base(message, inner)
        {
            Messages.Add(message);
        }
        
        public BaseException(IList<string> messages)
        {
            foreach (var message in messages)
            {
                Messages.Add(message);
            }
        }
    }
}