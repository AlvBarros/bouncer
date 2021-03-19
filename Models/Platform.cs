using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using bouncer.Repositories;

namespace bouncer.Models
{
    public class Platform : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        [JsonIgnore]
        public int OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }
    }
}