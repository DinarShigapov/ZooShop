using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Admin.Model
{
    public partial class Employee
    {
        public string StrFullName
        {
            get
            {
                return $"{LastName} {FirstName.ToCharArray()[0]}. {Patronymic.ToCharArray()[0]}.";
            }
        }

       
    }
}
