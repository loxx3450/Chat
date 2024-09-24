using System;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json.Serialization;

namespace CommonLibrary.Models.EF;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool VerifiedEmail { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();

    public virtual ICollection<Dialogue> Dialogues { get; set; } = new List<Dialogue>();


    [JsonConstructor]
    public User(string username, string email, string password)
    {
        Username = username;
        Email = email;
        Password = password;

        //Default values
        VerifiedEmail = false;

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
