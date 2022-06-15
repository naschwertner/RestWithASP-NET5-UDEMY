﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Books
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("lauch_date")]
        public string LauchDate { get; set; }

        [Column("price")]
        public string Price { get; set; }

        [Column("title")]
        public string Title { get; set; }
        
    }
}

