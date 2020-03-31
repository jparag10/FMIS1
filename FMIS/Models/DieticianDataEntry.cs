using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FMIS.Models
{
    public class DieticianDataEntry
    {
        [Key]
        public int ddeID { get; set; }
        public Dietician Dietician { get; set; }
        public Diseases Disease { get; set; }
        public string WhatToEat { get; set; }
        public string NotToEat { get; set; }
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