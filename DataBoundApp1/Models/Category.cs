using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBoundApp1.Models
{
  public  class Category
    {
     
          public int category_id { get; set; }
          public string display_category_name { get; set; }
          public string english_category_name { get; set; }
          public string url_category_name { get; set; }
          public string display_category_name_upper {
              get { return display_category_name.ToUpper(); } }
      
        
    }
}
