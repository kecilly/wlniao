using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Dao
{
    public interface IKeyValueDataDao : IRepository<Domain.KeyValueData>
    {
        IQueryable<Domain.KeyValueData> LoadAllByPage(out long total, int page, int rows, string order, string sort);
        Domain.KeyValueData GetByKeyName(string KeyName);
    }
}
