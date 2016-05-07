using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MemberLoginSSRS.DTO;
using MemberLoginSSRS.Helpers;

namespace MemberLoginSSRS.Controllers
{
    public class LayoutController : ApiController
    {
        [HttpPost]
        [ResponseType(typeof(LayoutRequestDTO))]

        public IHttpActionResult Post(LayoutRequestDTO layoutDto)
        {
            var data = ReportHelper.SaveLayout(layoutDto.Layout, User.Identity.Name);
            return Ok(data);
        }


        public IHttpActionResult Get()
        {
            var data = ReportHelper.GetLayout(User.Identity.Name);
            return Ok(data);
        }
    }
}
