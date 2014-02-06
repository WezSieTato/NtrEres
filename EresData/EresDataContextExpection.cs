using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EresData
{
    public class EresDataContextException : ApplicationException
    {
            public EresDataContextException()
    {
        this.HelpLink = "https://studia.elka.pw.edu.pl/priv/13Z/NTR.A/lab4.pdf";
    }
            public EresDataContextException(string sth, string typ) : base(sth)
    {
        this.HelpLink = "https://studia.elka.pw.edu.pl/priv/13Z/NTR.A/lab4.pdf";
        this.OperationType = typ;
    }

            public string OperationType{ get; set;} 

    }
}
