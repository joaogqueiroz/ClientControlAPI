﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetAPIProject01.Services.Models
{
    public class ClientEditModel
    {
        [Required(ErrorMessage = "Please, inform the client ID.")]
        public Guid ClientID { get; set; }
        [Required(ErrorMessage = "Please, inform the client name.")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Please, inform a valid email.")]
        [Required(ErrorMessage = "Please, inform the client email.")]
        public string Email { get; set; }
    }
}
