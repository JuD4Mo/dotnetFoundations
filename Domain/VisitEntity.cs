namespace Domain
{
  public class VisitEntity
  {
    public Guid Id {get; private set; }
    public Guid PersonId {get; private set; }
    public DateTime EntryTime {get; private set; }
    public DateTime? ExitTime {get; private set; }
    public PersonEntity? Person {get; private set; }

    public bool isActive => ExitTime == null;
    public TimeSpan? Duration => ExitTime.HasValue ? ExitTime.Value - EntryTime : null;

    private VisitEntity() { }

    public VisitEntity(Guid personId, DateTime? entryTime = null)
    {
      if (personId == Guid.Empty)
      {
        throw new ArgumentException("El id de la persona es inválido", nameof(personId));
      }

      Id = Guid.NewGuid();
      PersonId = personId;
      EntryTime = entryTime ?? DateTime.UtcNow;
      ExitTime = null;

    }

    public void RegisterExit(DateTime? exitTime = null)
    {
      var exit = exitTime ?? DateTime.UtcNow;
    
      if (ExitTime.HasValue)
      {
        throw new InvalidOperationException("La hora de salida debe ser posterior a la hora de entrada");
      }

      if (exit <= EntryTime)
      {
        throw new ArgumentException("La hora de salida debe ser posterior a la hora de entrada", nameof(exitTime)); 
      }

      ExitTime = exit;
    }

  }
}
