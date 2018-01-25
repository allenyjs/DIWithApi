using System.Collections.Generic;
using System.Collections.Specialized;
using DIWithApi.Model;

namespace DIWithApi.Interface
{
    public interface IGoodsInfo: ICheckResult
    {
        GoodsInfo Get();
        List<GoodsInfo> GetAll();
        verifyResult Create();
        verifyResult Edit();
        verifyResult Delete();
        NameValueCollection NVC { get; set; }
    }
}
