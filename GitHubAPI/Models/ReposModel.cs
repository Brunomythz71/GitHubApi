using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GitHubAPI.Models
{
  public class ReposModel
  {
    public string Imagem { get; set; }
    public string NomeCompleto { get; set; }
    public string DescricaoRepositorio { get; set; }
    public DateTime DataCriacao { get; set; }
    public string linguagemRepositorio { get; set; }
  }
}