using System;
using System.Collections.Generic;
using System.Runtime;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Mediateur.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Mediateur.Models
{
    public class ApplicationUser : IdentityUser
    {


        public string FirstName { get; set; }

        public string LastName { get; set; }


    }
}
