using DIWithApi.Model;

namespace DIWithApi.Service
{
    public class ServiceBase
    {
        public verifyResult verifyModel = new verifyResult();
        public ApiClient<verifyResult> _verifyResult = new ApiClient<verifyResult>();
        public void verifyModelDefaultSet()
        {
            verifyModel.VeryfyResult = false;
            verifyModel.msg = string.Empty;
        }
    }
}
