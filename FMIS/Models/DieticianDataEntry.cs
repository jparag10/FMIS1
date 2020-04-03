using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FMIS.Models
{
    public class DieticianDataEntry
    {
        [Key]
        public int ddeID { get; set; }
        
        public string Disease { get; set; }
        public string WhatToEat { get; set; }
        public string NotToEat { get; set; }

        public int? dieticianid { get; set; }
      

    }  
}