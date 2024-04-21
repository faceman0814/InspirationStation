namespace FaceMan.Utils.Auditing;

/// <summary>
/// 用于禁用单个方法的审计或禁用整个类或接口的所有方法的审计。
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class DisableAuditingAttribute : Attribute
{
}