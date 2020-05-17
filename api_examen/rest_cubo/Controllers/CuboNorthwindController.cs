using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace rest_cubo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods:"*")]
    [RoutePrefix("v1/Analisis/Northwind")]
    public class CuboNorthwindController : ApiController
    {
        [HttpGet]
        [Route("Top5")]

        public HttpResponseMessage Top5()
        {
            string dimension = "[Dim Cliente].[Dim Cliente Nombre].CHILDREN";
            string WITH = @"
                WITH
                SET [TopVentas] AS NONEMPTY(
                    ORDER(
                    )
                )
                "
            return Request.CreateErrorResponse(HttpStatusCode.OK, "");
        }

    }
}
