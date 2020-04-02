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
        //public Dietician Dietician { get; set; }
        //public Dietician did { get; set; }
        public Diseases Disease { get; set; }
        public string WhatToEat { get; set; }
        public string NotToEat { get; set; }

        public int? dieticianid { get; set; }
        //[ForeignKey("dieticianid")]
        //public virtual Dietician Dietician { get; set; }
        //public virtual ICollection<Dietician> Dieticians { get; set; }

    }
    public enum Diseases
    {
        Diabeties,
        Low_Blood_Pressure,
        High_Blood_Pressure,
        Fever_Cold_Cough,
        Fever,
        Dengue,
        Malaria,
        Piles,
        TB,
        Cancer
    }

}