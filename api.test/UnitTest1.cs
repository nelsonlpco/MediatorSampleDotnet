using System;
using System.Linq;
using System.ComponentModel;
using Xunit;
using MediatorSample.Domain;
using MediatorSample.Application;
using System.Threading;
using FluentAssertions;

namespace api.test
{
  public class UnitTest1
  {
    [Fact(DisplayName = @"
            DADO
                que Antonio escalou o time da Qualyteam
            QUANDO
                Durante o jogo ele decide fazer uma substituição
            ENTÃO
                Quando arbitro Ivan levanta a banderia
                E Leonardo sai de campo
                E Marcel entra em campo
        ")]
    public async void Test1()
    {
      var tecnico = new Tecnico("Antonio");

      var command = new SubstituirJogadorCommand(
          tecnico,
          tecnico.Time.FirstOrDefault(j => j.Nome == "Leonardo"),
          tecnico.Time.FirstOrDefault(j => j.Nome == "Marcel"),
          new QuartoArbitro("Ivan")
      );

      var commandHandler = new SubstituirJogadorCommandHandler();

      var time = await commandHandler.Handle(command, new CancellationToken());
      time.FirstOrDefault(j => j.Nome == "Marcel").EstaEmCampo.Should().BeTrue();
      time.FirstOrDefault(j => j.Nome == "Leonardo").EstaEmCampo.Should().BeFalse();

    }
  }
}
