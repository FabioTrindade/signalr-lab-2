using System.ComponentModel.DataAnnotations;

namespace signalr_hub.Entities;

public abstract record Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        Active = true;
        CreatedAt = DateTime.Now;
    }


    [Key]
    public Guid Id { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime? UpdatedAt { get; private set; }

    public bool Active { get; private set; }


    public void SetUpdatedAt() => UpdatedAt = DateTime.Now;

    public void SetActive(bool active) => Active = active;

}