using System.Runtime.Serialization;

namespace FaceMan.Utils.Exception;

[Serializable]
public class EntityNotFoundException : System.Exception
{
    public Type EntityType { get; set; }

    /// <summary>Id of the Entity.</summary>
    public object Id { get; set; }

    
    public EntityNotFoundException()
    {
    }

  
    public EntityNotFoundException(SerializationInfo serializationInfo, StreamingContext context)
        : base(serializationInfo, context)
    {
    }

  
    public EntityNotFoundException(Type entityType, object id)
        : this(entityType, id, (System.Exception) null)
    {
    }

    public EntityNotFoundException(Type entityType, object id, System.Exception innerException)
        : base(string.Format("There is no such an entity. Entity type: {0}, id: {1}", (object) entityType.FullName, id), innerException)
    {
        this.EntityType = entityType;
        this.Id = id;
    }

    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    public EntityNotFoundException(string message, System.Exception innerException)
    {
    }
}