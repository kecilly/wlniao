using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shijia.Service
{
    public interface IMPMenuService : IGenericManager<Domain.MPMenu>
    {
        Shijia.Dao.IMPMenuDao MPMenuDao { set; get; }
        int GetNewSequense(Int32 AccountId);
        void UpdateSeed(Int32 AccountId, Int32 Sequense);
        String GetTreeData(Int32 AccountId);
        String GetMenuData(Int32 AccountId);
        int SaveTreeData(Int32 AccountId, string data);
        int SaveMenuData(Int32 AccountId, string data);
    }
}
