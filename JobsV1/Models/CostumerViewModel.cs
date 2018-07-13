﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobsV1.Models
{
    public class CostumerDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact1 { get; set; }
        public string Contact2 { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
        public int    JobID { get; set; }
        public int    CustCategoryID { get; set; }
        public string CustCategoryIcon { get; set; }
        public int    CustEntID { get; set; }
        public string CustEntName { get; set; }
        public string CustEntIconPath { get; set; }
    }

    public class CostumerViewModel
    {

    }
}