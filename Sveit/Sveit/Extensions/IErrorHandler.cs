using System;
using System.Collections.Generic;
using System.Text;

namespace Sveit.Extensions
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
