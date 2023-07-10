using FlightMobileApp.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FlightMobileApp.models
{
    public class Command
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Command_id { get; set; }

        [JsonPropertyName("aileron")]
        public double Aileron { get; set; }
        [JsonPropertyName("rudder")]
        public double Rudder { get; set; }

        [JsonPropertyName("elevator")]
        public double Elevator { get; set; }

        [JsonPropertyName("throttle")]
        public double Throttle { get; set; }
    }
    public class CommandContext : DbContext
    {
        public CommandContext(DbContextOptions<CommandContext> options) : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }

    }
}

