namespace Dnd_Api.DTO
{
	public record UserDto(
		int Id,
		string Name,
		int RoleId,
		string RoleName
	);

	public record UpdateUserRoleDto(int RoleId);
}
