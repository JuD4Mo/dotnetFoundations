using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
  public class GetPersonByCodeUseCase
  {
    private readonly ICodeRepository<PersonEntity> _codeRepository; //private element
  
    public GetPersonByCodeUseCase(ICodeRepository<PersonEntity> codeRepository)
    {
      _codeRepository = codeRepository;
    }

    public async Task<PersonEntity> ExecuteAsync(string code)
    {
      var person = await _codeRepository.GetByCodeAsync(code);
      if (person == null)
      {
        throw new InvalidOperationException($"No se encontró una persona con el código: {code}");
      }

      return person;
    }
  }
}