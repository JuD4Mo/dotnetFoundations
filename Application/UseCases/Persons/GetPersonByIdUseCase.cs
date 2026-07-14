using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
  public class GetPersonByIdUseCase
  {
    private readonly IRepository<PersonEntity, Guid> _repository; //private element
  
    public GetPersonByIdUseCase(IRepository<PersonEntity, Guid> repository)
    {
      _repository = repository;
    }

    public async Task<PersonEntity> ExecuteAsync(Guid id)
    {
      var person = await _repository.GetByIdAsync(id);
      if (person == null)
      {
        throw new InvalidOperationException($"No se encontró una persona con el id: {id}");
      }

      return person;
    }
  }
}