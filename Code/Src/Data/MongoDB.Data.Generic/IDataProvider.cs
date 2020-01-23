using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Data.Generic
{
    /// <summary>
    /// Data provider interface
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// Initialize database
        /// </summary>
        void InitDatabase();

    }
}
