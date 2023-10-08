using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finance.CurrentAssetModule.Store.Model.Settings
{
    public enum StoreTransTypeEnum
    {
        Start = 5,
        Buy = 10,
        Sale = 20,
        SalesReturn= 30,
        BuyReturn = 40,
        ChangeLocation=50,
        Manufacturing = 60,
        Destory = 70,
    }
}
