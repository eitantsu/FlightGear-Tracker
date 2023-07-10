using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace FlightMobileApp.models
{
    public class Screenshot
    {
        [Key]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Screenshot_id { get; set; }
        [JsonPropertyName("screenshot")]
        public byte[] Img { get; set; }
    }
    public class ScreenshotContext : DbContext
    {
        public ScreenshotContext(DbContextOptions<ScreenshotContext> options) : base(options)
        {
        }

        public DbSet<Screenshot> Screenshots { get; set; }

    }
}
