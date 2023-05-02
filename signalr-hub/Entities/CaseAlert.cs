using signalr_hub.Enums;

namespace signalr_hub.Entities;

public record CaseAlert : Entity
{
    public CaseAlert(string? name
        , string? description
        , GroupEnum group)
    {
        Name = name;
        Description = description;
        Group = group;
    }


    public string? Name { get; private set; }

    public string? Description { get; private set; }

    public string? Analyst { get; private set; }

    public GroupEnum Group { get; private set; }


    public void SetAnalyst(string analyst) => Analyst = analyst;
}