namespace FaceMan.Utils.Entities;

public interface IFullAuditedEntity<TPrimaryKey> : IEntity<TPrimaryKey>,ICreationAudited, IDeletionAudited,
    IModificationAudited
{
}