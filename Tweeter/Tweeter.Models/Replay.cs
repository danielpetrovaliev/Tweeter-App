using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Tweeter.Models
{
    public class Replay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public User Author { get; set; }
    }
}
