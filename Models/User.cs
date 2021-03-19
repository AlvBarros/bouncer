using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using bouncer.DTO.User;
using bouncer.Repositories;

namespace bouncer.Models
{
    public class User : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [JsonIgnore]
        public string Password { get; set; }
        public DateTimeOffset Joined { get; set; }
        public DateTimeOffset LastLogin { get; set; }
        [JsonIgnore]
        public List<Platform> Platforms { get; set; }

        public static User FromRegisterRequest(RegisterRequest request)
        {
            var requestUser = new User()
            {
                Email = request.Email.ToLower(),
                Password = request.Password,
                Joined = DateTimeOffset.UtcNow,
                Platforms = new List<Platform>()
            };
            return requestUser;
        }
    }
}