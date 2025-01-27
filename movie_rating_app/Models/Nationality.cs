﻿using System;
using System.Collections.Generic;

namespace movie_rating_app.Models
{
    public partial class Nationality
    {
        public Nationality()
        {
            Actors = new HashSet<Actor>();
      //      AspNetUsers = new HashSet<AspNetUser>();
            Creators = new HashSet<Creator>();
        }

        public string Name { get; set; } = null!;
        public int Id { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
   //     public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
        public virtual ICollection<Creator> Creators { get; set; }
    }
}
