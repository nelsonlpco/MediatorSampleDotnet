using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatorSample.Domain;
using MediatorSample.Domain.DTOs;
using MediatorSample.Application;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorSample.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TimeController : ControllerBase
  {
    private readonly IMediator _mediator;
    public TimeController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<IEnumerable<Jogador>>> Post([FromBody] SubstituicaoDTO dto)
    {
      var tecnico = new Tecnico(dto.TecnicoDTO.Nome);
      var jogadorEmCampo = new Jogador(dto.JogadorEmCampoDTO.Nome, 11, true);
      var jogadorNoCampo = new Jogador(dto.JogadorNoBancoDTO.Nome, 13, false);
      var quartoArbitro = new QuartoArbitro(dto.QuartoArbitroDTO.Nome);

      var command = new SubstituirJogadorCommand(tecnico, jogadorEmCampo, jogadorNoCampo, quartoArbitro);

      var resultado = await _mediator.Send(command);
      return Ok(resultado);
    }
  }
}
