using System.ComponentModel.DataAnnotations.Schema;
using FaceManUtils.Entities;

namespace Core.UserModule;

[Table(AppConsts.TablePrefix + "User")]
public class User: FullAuditedEntity<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public required string Password { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }
    /// <summary>
    /// 手机号
    /// </summary>
    public string? PhoneNumber { get; set; }
    /// <summary>
    /// 组织单元
    /// </summary>
    public string? OrganizationUnitId { get; set; }
    /// <summary>
    /// 外键-组织机构
    /// </summary>
    public OrganizationUnit OrganizationUnit { get; set; }
}