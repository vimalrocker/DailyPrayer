
using System.ComponentModel.DataAnnotations;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace Api.Models;

[Table("users")]
public class UserProfile : BaseModel
{
    [PrimaryKey("id")]
    public string Id { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [Column("email")]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    [Column("password_hash")]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    [Column("display_name")]
    public string DisplayName { get; set; } = string.Empty;
    
    [Column("avatar_url")]
    public string? AvatarUrl { get; set; }
    
    [Column("is_verified")]
    public bool IsVerified { get; set; } = false;
    
    [Column("is_pro")]
    public bool IsPro { get; set; } = false;
    
    [Column("pro_since")]
    public DateTime? ProSince { get; set; }
    
    [Column("pro_expires")]
    public DateTime? ProExpires { get; set; }
    
    [Column("daily_notification_time")]
    public TimeSpan DailyNotificationTime { get; set; } = TimeSpan.FromHours(13).Add(TimeSpan.FromMinutes(46)).Add(TimeSpan.FromSeconds(32));
    
    [Column("theme_preference")]
    public string ThemePreference { get; set; } = "light";
    
    [Column("font_size_preference")]
    public string FontSizePreference { get; set; } = "medium";
    
    [Column("language_code")]
    public string LanguageCode { get; set; } = "en";
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Column("last_login")]
    public DateTime? LastLogin { get; set; }
    
    [Column("is_deleted")]
    public bool IsDeleted { get; set; } = false;
}