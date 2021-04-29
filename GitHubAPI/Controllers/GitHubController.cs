using GitHubAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace GitHubAPI.Controllers
{
  [RoutePrefix("v1/github")]
  public class GitHubController : ApiController
  {
    [HttpPost]
    [Route("getRepos")]
    public async Task<IHttpActionResult> IntegrarAPIGitHub()
    {
      try
      {
        var _configuration = ConfigurationManager.AppSettings;

        List<ReposModel> repositorio = new List<ReposModel>();
        using (var client = new HttpClient())
        {
          client.DefaultRequestHeaders.UserAgent.TryParseAdd("request");

          var url = string.Format("https://api.github.com/orgs/{0}/repos", _configuration[$"GitHub:Repos"]);

          HttpResponseMessage response = client.GetAsync(url).Result;

          response.EnsureSuccessStatusCode();
          string conteudo = response.Content.ReadAsStringAsync().Result;

          dynamic resultado = JsonConvert.DeserializeObject(conteudo);

          foreach (var item in resultado)
          {
            ReposModel repos = new ReposModel();

            repos.NomeCompleto = item.full_name;
            repos.DescricaoRepositorio = item.description;
            repos.Imagem = item.owner.avatar_url;
            repos.DataCriacao = item.created_at;
            repos.linguagemRepositorio = item.language.ToString();

            repositorio.Add(repos);
          }

          return Ok(repositorio.Where(b => b.linguagemRepositorio.Equals("C#")).OrderBy(a => a.DataCriacao).Take(5).ToList());
        }
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}