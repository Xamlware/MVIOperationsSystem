using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MVIOperationsSystem.Services
{
	public class ExecuteDataRequest
	{
		public async Task<String> ExecuteRequest(string route, HttpRequestMethods method, string content)
		{
			string result = null;
			using (var client = new System.Net.Http.HttpClient())
			{
				try
				{
					// Setting Base address.  
					client.BaseAddress = new Uri("https://localhost:44375/");

					// Setting content type.  
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					// Setting timeout.  
					client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

					HttpResponseMessage response = new HttpResponseMessage();
					switch (method)
					{
						case HttpRequestMethods.Post:
							var pContent = new StringContent(content, Encoding.UTF8, "application/json");
							response = await client.PostAsync(route, pContent).ConfigureAwait(false);
							break;
						case HttpRequestMethods.Put:
							break;
						case HttpRequestMethods.Delete:
							break;
						case HttpRequestMethods.Get:
							response = await client.GetAsync(route);
							break;
						default:
							break;
					}

					// Verification  
					if (response.IsSuccessStatusCode)
					{
						 result = response.Content.ReadAsStringAsync().Result;

						// Releasing.  
						response.Dispose();
					}
					else
					{
						// Reading Response.  
						result = response.Content.ReadAsStringAsync().Result;
						//responseObj.code = 602;
					}

				}

				catch (Exception ex)
				{
					throw ex;
				}

				return result;
			}
		}
	}
}
