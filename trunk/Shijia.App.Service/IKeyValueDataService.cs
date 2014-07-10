using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shijia.App.Domain;

namespace Shijia.App.Service
{
    public interface IKeyValueDataService : IGenericManager<Domain.KeyValueData>
    {
        Shijia.App.Dao.IKeyValueDataDao KeyValueDataDao { set; get; }
        KeyValueData GetByKey(string key);


        void Add(string key, string value);
        void Add(string key, string value, string description);
        void Edit(string key, string value);
        void Edit(string key, string value, string description);
        void Save(string key, string value);
        String GetString(string key);
        Int32 GetInt(string key);
        Boolean GetBool(string key);
    }
}
