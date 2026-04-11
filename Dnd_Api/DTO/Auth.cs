namespace Dnd_Api.DTO
{
	public record LoginDto(
		string Name,
		string Password
	);

	public record RegisterDto(
		string Name,
		string Password
	);

	public record AuthResponseDto(
		string Token,
		string Username,
		string Role,
		DateTime ExpiresAt
	);
}
