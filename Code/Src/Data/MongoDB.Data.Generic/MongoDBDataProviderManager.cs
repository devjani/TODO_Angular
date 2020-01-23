﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.Data.Generic
{
    public partial class MongoDBDataProviderManager : BaseDataProviderManager
    {
        public MongoDBDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        public override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
            if (!String.IsNullOrWhiteSpace(providerName) && providerName.ToLowerInvariant() != "mongodb")
                throw new Exception(string.Format("Not supported dataprovider name: {0}", providerName));

            return new MongoDBDataProvider();
        }
    }
}
