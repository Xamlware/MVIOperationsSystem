using MVIOperations.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MVIOperationsSystem.DataServices
{
    public class DataRequest
    {
    }


    //public static async Task<LoginRequest> PostRegInfo(LoginRequest requestObj)
    //{
    //    // Initialization.  
    //   // RegInfoResponseObj responseObj = new RegInfoResponseObj();

    //    try
    //    {
    //        // Posting.  
    //        using (var client = new HttpClient())
    //        {
    //            // Setting Base address.  
    //            client.BaseAddress = new Uri("http://localhost:19006/");

    //            // Setting content type.  
    //            client.DefaultRequestHeaders.Accept.Clear();
    //            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    //            // Setting timeout.  
    //            client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

    //            // Initialization.  
    //            HttpResponseMessage response = new HttpResponseMessage();

    //            // HTTP POST  
    //            response = await client.PostAsync("api/WebApi/PostRegInfo", requestObj).ConfigureAwait(false);

    //            // Verification  
    //            if (response.IsSuccessStatusCode)
    //            {
    //                // Reading Response.  
    //                string result = response.Content.ReadAsStringAsync().Result;
    //                responseObj = JsonConvert.DeserializeObject<RegInfoResponseObj>(result);

    //                // Releasing.  
    //                response.Dispose();
    //            }
    //            else
    //            {
    //                // Reading Response.  
    //                string result = response.Content.ReadAsStringAsync().Result;
    //                responseObj.code = 602;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }

    //    return responseObj;
    //}
}      
