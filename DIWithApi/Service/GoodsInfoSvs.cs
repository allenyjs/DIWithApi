using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using DIWithApi.Model;
using DIWithApi.Service;
using DIWithApi.Interface;


namespace DIWithApi
{
    public class GoodsInfoSvs:ServiceBase,IGoodsInfo
    {
        public GoodsInfo goodsInfo { get; set; }
        public List<GoodsInfo> goodsInfoList { get; set; }
        public NameValueCollection NVC { get; set; }
        public GoodsInfoSvs()
        {
            goodsInfo = new GoodsInfo();
        }
        public GoodsInfo Get()
        {
            ApiClient<GoodsInfo> _ApiClinet = new ApiClient<GoodsInfo>();
            verifyModelDefaultSet();
            try
            {
                _ApiClinet.nvc = NVC;
                goodsInfo = _ApiClinet.ApiResult(_ApiClinet.ApiPath("GoodsInfo"), goodsInfo);
                if (goodsInfo != null)
                {
                    verifyModel.VeryfyResult = true;
                }
            }
            catch (Exception ex)
            {
                verifyModel.msg = ex.Message;
                goodsInfo = new GoodsInfo();
            }
            return goodsInfo;
        }

        public List<GoodsInfo> GetAll()
        {
            ApiClient<List<GoodsInfo>> _ApiClinet = new ApiClient<List<GoodsInfo>>();
            verifyModelDefaultSet();
            try
            {
                goodsInfoList = _ApiClinet.ApiResult(_ApiClinet.ApiPath("GoodsInfo"), goodsInfoList);
                if (goodsInfoList != null && goodsInfoList.Any())
                {
                    verifyModel.VeryfyResult = true;
                }
            }
            catch (Exception ex)
            {
                verifyModel.msg = ex.Message;
                goodsInfoList = new List<GoodsInfo>();
            }
            return goodsInfoList;
        }
        public verifyResult Create()
        {
            verifyModelDefaultSet();
            _verifyResult.nvc = NVC;
            return _verifyResult.ApiResult(_verifyResult.ApiPath("GoodsInfo"), verifyModel, HttpMethod.Post, SendFormat.FromBody);
        }
        public verifyResult Edit()
        {
            verifyModelDefaultSet();
            _verifyResult.nvc = NVC;
            return _verifyResult.ApiResult(_verifyResult.ApiPath("GoodsInfo?Id=1"), verifyModel, HttpMethod.Put, SendFormat.FromBody);
        }
        public verifyResult Delete()
        {
            verifyModelDefaultSet();
            _verifyResult.nvc = NVC;
            return _verifyResult.ApiResult(_verifyResult.ApiPath("GoodsInfo"), verifyModel, HttpMethod.Delete);
        }
        public verifyResult CheckResult()
        {
            return verifyModel;
        }
    }
}