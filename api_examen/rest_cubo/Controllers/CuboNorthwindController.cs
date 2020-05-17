using Microsoft.AnalysisServices.AdomdClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace rest_cubo.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                SET [TopVentas] AS
                NONEMPTY(
                    ORDER(
                        STRTOSET(@Dimension),
                        [Measures].[Fact Ventas Netas], DESC
                    )
                )
            ";
            string COLUMNS = @"
                NON EMPTY
                {
                    [Measures].[Fact Ventas Netas]
                }
                ON COLUMNS,    
            ";
            string ROWS = @"
                NON EMPTY
                {
                    HEAD([TopVentas], 5)
                }
                ON ROWS
            ";
            string CUBO_NAME = "[DWHNorthwind]";
            string MDX_QUERY = WITH + @"SELECT " + COLUMNS + ROWS + " FROM " + CUBO_NAME;
            Debug.Write(MDX_QUERY);

            List<string> clients = new List<string>();
            List<decimal> ventas = new List<decimal>();
            List<dynamic> lstTabla = new List<dynamic>();

            dynamic result = new
            {
                datoDimension = clients,
                datosVenta = ventas,
                datosTabla = lstTabla
            };

            using (AdomdConnection cnn = new AdomdConnection(ConfigurationManager.ConnectionStrings["CuboNorthwind"].ConnectionString))
            {
                cnn.Open();
                using (AdomdCommand cmd = new AdomdCommand(MDX_QUERY, cnn))
                {
                    cmd.Parameters.Add("Dimension", dimension);
                    using (AdomdDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            clients.Add(dr.GetString(0));
                            ventas.Add(Math.Round(dr.GetDecimal(1)));

                            dynamic objTabla = new
                            {
                                descripcion = dr.GetString(0),
                                valor = Math.Round(dr.GetDecimal(1))
                            };
                            lstTabla.Add(objTabla);
                        }
                        dr.Close();
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, (object)result);
        }

        [HttpGet]
        [Route("AnosVentas")]
        public HttpResponseMessage AnosVentas()
        {
            string COLUMNS = @"
                NON EMPTY
                {
                    [Measures].[Fact Ventas Netas]
                }
                ON COLUMNS,    
            ";
            string ROWS = @"
                NON EMPTY
                {
                    [Dim Tiempo].[Dim Tiempo Año].Children
                }
                ON ROWS
            ";
            string CUBO_NAME = "[DWHNorthwind]";
            string MDX_QUERY = @"SELECT " + COLUMNS + ROWS + " FROM " + CUBO_NAME;
            Debug.Write(MDX_QUERY);

            List<string> anos = new List<string>();
            List<decimal> ventas = new List<decimal>();

            dynamic result = new
            {
                datoDimension = anos,
                datosVenta = ventas
            };

            using (AdomdConnection cnn = new AdomdConnection(ConfigurationManager.ConnectionStrings["CuboNorthwind"].ConnectionString))
            {
                cnn.Open();
                using (AdomdCommand cmd = new AdomdCommand(MDX_QUERY, cnn))
                {
                    using (AdomdDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            anos.Add(dr.GetString(0));
                            ventas.Add(Math.Round(dr.GetDecimal(1)));
                        }
                        dr.Close();
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, (object)result);
        }

        [HttpGet]
        [Route("NombreDim/{dim}")]
        public HttpResponseMessage NombreDim(string dim)
        {
            string dimension = "";
            switch (dim)
            {
                case "Cliente":
                    dimension = "[Dim Cliente].[Dim Cliente Nombre].Children";
                    break;
                case "Producto":
                    dimension = "[Dim Producto].[Dim Producto Nombre].Children";
                    break;
                case "Empleado":
                    dimension = "[Dim Empleado].[Dim Empleado Nombre].Children";
                    break;
                default:
                    dimension = "[Dim Cliente].[Dim Cliente Nombre].Children";
                    break;

            }


            string COLUMNS = @"
                NON EMPTY
                {
                    [Measures].[Fact Ventas Netas]
                }
                ON COLUMNS,    
            ";

            string CUBO_NAME = "[DWHNorthwind]";
            string MDX_QUERY = @"SELECT " + COLUMNS + @" NON EMPTY  { " + dimension + "  }ON ROWS FROM " + CUBO_NAME;
            Debug.Write(MDX_QUERY);

            List<string> meses = new List<string>();
            List<decimal> ventas = new List<decimal>();

            dynamic result = new
            {
                datoDimension = meses,
                datosVenta = ventas
            };

            using (AdomdConnection cnn = new AdomdConnection(ConfigurationManager.ConnectionStrings["CuboNorthwind"].ConnectionString))
            {
                cnn.Open();
                using (AdomdCommand cmd = new AdomdCommand(MDX_QUERY, cnn))
                {
                    using (AdomdDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            meses.Add(dr.GetString(0));
                            ventas.Add(Math.Round(dr.GetDecimal(1)));
                        }
                        dr.Close();
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, (object)result);
        }

        [HttpGet]
        [Route("MesesVentas")]
        public HttpResponseMessage MesesVentas()
        {
            string COLUMNS = @"
                NON EMPTY
                {
                    [Measures].[Fact Ventas Netas]
                }
                ON COLUMNS,    
            ";
            string ROWS = @"
                NON EMPTY
                {
                    [Dim Tiempo].[Dim Tiempo Mes].Children
                }
                ON ROWS
            ";
            string CUBO_NAME = "[DWHNorthwind]";
            string MDX_QUERY = @"SELECT " + COLUMNS + ROWS + " FROM " + CUBO_NAME;
            Debug.Write(MDX_QUERY);

            List<string> meses = new List<string>();
            List<decimal> ventas = new List<decimal>();

            dynamic result = new
            {
                datoDimension = meses,
                datosVenta = ventas
            };

            using (AdomdConnection cnn = new AdomdConnection(ConfigurationManager.ConnectionStrings["CuboNorthwind"].ConnectionString))
            {
                cnn.Open();
                using (AdomdCommand cmd = new AdomdCommand(MDX_QUERY, cnn))
                {
                    using (AdomdDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (dr.Read())
                        {
                            meses.Add(dr.GetString(0));
                            ventas.Add(Math.Round(dr.GetDecimal(1)));
                        }
                        dr.Close();
                    }
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, (object)result);
        }

    }
}
