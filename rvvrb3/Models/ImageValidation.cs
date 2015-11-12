using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace rvvrb3.Models
{
    public class ImageValidation : ValidationAttribute
    {
        public int MaxLength { get; set; }

        public string[] Allowed { get; set; }

        public override bool IsValid(object value)
        {

            var file = value as HttpPostedFileBase;

            if (file.ContentLength > MaxLength)
            {
                return false;
            }

            if (!Allowed.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
            {
                return false;
            }

            return true;
        }
    }
}