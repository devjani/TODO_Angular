using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Data.Generic
{
    public class MongoDBDataProvider : IDataProvider
    {
        #region Methods


        /// <summary>
        /// Initialize database
        /// </summary>
        public virtual void InitDatabase()
        {
            //DataSettingsHelper.InitConnectionString();
        }

        #endregion
    }
}
