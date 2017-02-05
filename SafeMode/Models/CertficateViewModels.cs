using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SafeMode.Models
{
    public class CertficateCreateModel
    {

        //public int id { get; set; }
        [Required(ErrorMessage = "Please enter name of the certificate")]
        public string name { get; set; }
        //public string urlname { get; set; }
        //public string uploadedby { get; set; }
        //public System.DateTime uploadedon { get; set; }

        [Required(ErrorMessage = "Please select the type of the certificate")]
        public Nullable<int> typeid { get; set; }

        [Required(ErrorMessage = "Please select the assignee for the certificate")]
        public string assigneeid { get; set; }

        [Required(ErrorMessage = "Please select a file of the certificate")]
        public HttpPostedFileBase file { get; set; }
    }

    public class CertficateEditModel
    {
    }
}