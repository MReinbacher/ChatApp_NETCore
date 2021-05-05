using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }
    }
}