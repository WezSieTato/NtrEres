//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EresData
{
    using System;
    using System.Collections.Generic;
    
    public partial class Semester
    {
        public Semester()
        {
            this.Realisations = new HashSet<Realisation>();
        }
    
        public int SemesterID { get; set; }
        public string Name { get; set; }
        public byte[] TimeStamp { get; set; }
    
        public virtual ICollection<Realisation> Realisations { get; set; }
    }
}
