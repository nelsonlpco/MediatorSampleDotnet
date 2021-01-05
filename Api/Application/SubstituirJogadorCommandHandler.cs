using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatorSample.Domain;
using MediatR;

namespace MediatorSample.Application
{
  public class SubstituirJogadorCommandHandler : IRequestHandler<SubstituirJogadorCommand, IEnumerable<Jogador>>
  {
    public Task<IEnumerable<Jogador>> Handle(SubstituirJogadorCommand request, CancellationToken cancellationToken)
    {
      Console.WriteLine(request.Tecnico.PedeSubstituicao(request.JogadorNoBanco.Nome, request.JogadorEmCampo.Nome));
      Console.WriteLine(request.QuartoArbitro.LevantaPlaca(request.JogadorNoBanco.Numero, request.JogadorEmCampo.Numero));

      return Task.FromResult(
        SubstituiJogador(
          request.Tecnico.Time,
          request.JogadorNoBanco.Nome,
          request.JogadorEmCampo.Nome
        )
      );
    }

    private IEnumerable<Jogador> SubstituiJogador(IEnumerable<Jogador> time, string nomeJogadorQueEntra, string nomeJogadorQueSai)
    {
      var timeAposSaida = JogadorSaiDoCampo(time, nomeJogadorQueSai);
      return JogadorEntraEmCampo(timeAposSaida, nomeJogadorQueEntra);
    }

    private IEnumerable<Jogador> JogadorSaiDoCampo(IEnumerable<Jogador> time, string nome)
    {
      foreach (var jogador in time)
      {
        if (jogador.Nome == nome)
        {
          jogador.SaiDeCampo();
        }

        yield return jogador;
      }
    }

    private IEnumerable<Jogador> JogadorEntraEmCampo(IEnumerable<Jogador> time, string nome)
    {
      foreach (var jogador in time)
      {
        if (jogador.Nome == nome)
        {
          jogador.EntraEmCampo();
        }

        yield return jogador;
      }
    }

  }
}
