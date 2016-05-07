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
    public class BookingInformationAPIController : ApiController
    {
        [ResponseType(typeof(BookingRequestDTO))]
        public IHttpActionResult PostPersonInformation(BookingRequestDTO bookingRequestDTO)
        {
            var data = DailyReportDataHelper.GetDailyData(bookingRequestDTO.Date);
            return Ok(data);
        }

    }
}
