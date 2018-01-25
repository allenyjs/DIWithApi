using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DIWithApi.Interface;
using DIWithApi.Model;

namespace DIWithApi.Controllers
{
    public class HomeController : Controller
    {
        private IGoodsInfo _GoodsInfo;
        public HomeController(IGoodsInfo goodsInfo)
        {
            _GoodsInfo = goodsInfo as IGoodsInfo;
        }
        public ActionResult Index()
        {
            List<GoodsInfo> GoodsInfos = _GoodsInfo.GetAll();
            return View();
        }

    }
}