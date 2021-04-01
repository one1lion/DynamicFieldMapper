using DynamicFieldMapper.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace DynamicFieldMapper.Client.Services {
  public class FieldMapperService {
    private readonly HttpClient httpClient;

    public FieldMapperService(HttpClient httpClient) {
      this.httpClient = httpClient;
    }

    public async Task<ResponseModel<ImportFile>> SaveTemplate(ImportFile model) {
      var retResp = new ResponseModel<ImportFile>();
      try {
        var resp = await httpClient.PostAsJsonAsync("api/Templates", model);

        if (resp.IsSuccessStatusCode) {
          return await resp.Content.ReadFromJsonAsync<ResponseModel<ImportFile>>();
        }
        retResp.AddError("The request was not successful.");
      } catch (Exception) {
        retResp.AddError("There was an error that occured while trying to save .");
      }
      return retResp;
    }

    public async Task<ResponseModel<List<ImportFile>>> SearchTemplates(List<string> headerNames) {
      var retResp = new ResponseModel<List<ImportFile>>();
      try {
        var resp = await httpClient.PostAsJsonAsync("api/Templates/Search", headerNames);

        if (resp.IsSuccessStatusCode) {
          return await resp.Content.ReadFromJsonAsync<ResponseModel<List<ImportFile>>>();
        }
        retResp.AddError("The request was not successful.");
      } catch (Exception) {
        retResp.AddError("There was an error that occured while trying to save .");
      }
      return retResp;
    }
  }
}
