using System.ComponentModel.DataAnnotations.Schema;
using FaceManUtils.Entities;

namespace Core.UserModule;

[Table(AppConsts.TablePrefix + "OrganizationUnit")]
public class OrganizationUnit : FullAuditedEntity<string>
{
    /// <summary>
    /// 名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 组织单元描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 上级组织单元ID，可为空表示顶级单位
    /// </summary>
    public string? ParentOrgUnitID { get; set; }

    /// <summary>
    /// 组织单元编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 组织单元状态
    /// </summary>
    public bool Status { get; set; } = true;
    
    // 父组织单元
    public virtual OrganizationUnit Parent { get; set; }
    
    // 子组织单元
    public virtual List<OrganizationUnit> Children { get; set; } = new List<OrganizationUnit>();
}