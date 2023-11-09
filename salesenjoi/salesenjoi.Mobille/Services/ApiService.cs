﻿using Newtonsoft.Json;
using salesenjoi.Mobille.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace salesenjoi.Mobille.Services
{
    public class ApiService : IApiService
    {
        public async Task<Response> PostAsync<T>(string urlBase, string servicePrefix, string controller, T model)
        {
			try
			{
				string request = JsonConvert.SerializeObject(model);
				StringContent content = new StringContent(request, Encoding.UTF8, "application/json");
                HttpClient cliente = new HttpClient
				{
					BaseAddress = new Uri(urlBase)	
				};

				string url = $"{servicePrefix}{controller}";
				HttpResponseMessage response = await cliente.PostAsync(url, content);
				string result = await response.Content.ReadAsStringAsync();

				if (!response.IsSuccessStatusCode)
				{
					return new Response
					{
						IsSuccess = false,
						Message = result

					};
				}

				T item = JsonConvert.DeserializeObject<T>(result);

				return new Response
				{ 
				  IsSuccess = true,
				  Result = item
				};

			}
			catch (Exception ex)
			{
				return new Response
				{
					IsSuccess = false,
					Message = ex.Message
				};
				
			}
        }
    }
}
