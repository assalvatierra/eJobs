using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace JobsV1.Controllers
{
    public interface IMainPage
    {
        ActionResult Index();
        ActionResult About();
        ActionResult Contact();
        ActionResult ApplicationPanel();
    }
}
