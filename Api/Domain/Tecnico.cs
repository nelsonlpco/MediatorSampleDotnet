using System;
using System.Collections.Generic;

namespace MediatorSample.Domain
{
  public class Tecnico
  {
    public Guid ID { get; private set; }
    public string Nome { get; private set; }
    public IEnumerable<Jogador> Time { get; private set; }

    public Tecnico(string nome)
    {
      ID = Guid.NewGuid();
      Nome = nome;
      Time = EscalaTime();
    }

    public string PedeSubstituicao(string nomeJogadorEntra, string nomeJogadorSai) =>
      $"Query substituir o jogador {nomeJogadorSai} pelo {nomeJogadorEntra}";

    private IEnumerable<Jogador> EscalaTime()
    {
      string[] nomes = new string[14] {
        "Leonardo", "Pedro", "marco","Juliano", "Rô",
        "Luis", "Leandro", "Antonio", "Romulo","Portuga",
        "Peralta", "Pedro Legacy", "Marcel", "Gabriel"
      };

      for (int i = 0; i < nomes.Length; i++)
      {
        if (i <= 11)
        {
          yield return new Jogador(nomes[i], i, true);
        }
        else
        {
          yield return new Jogador(nomes[i], i, false);
        }
      }
    }

  }
}
