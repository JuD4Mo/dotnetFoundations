using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using Domain.Abstractions;

namespace Application.UseCases.Persons
{
  public class GetAllPersonsUseCase
  {
    private readonly IRepository<PersonEntity, Guid> _repository; //private element

    public GetAllPersonsUseCase(IRepository<PersonEntity, Guid> repository)
    {
      _repository = repository;
    }

    public async Task<IEnumerable<PersonEntity>> ExecuteAsync()
    {
      return await _repository.GetAllAsync();
    }
  }
}