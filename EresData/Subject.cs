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
    
    public partial class Subject
    {
        public Subject()
        {
            this.Realisations = new HashSet<Realisation>();
        }
    
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public string Conspect { get; set; }
        public string url { get; set; }
        public byte[] TimeStamp { get; set; }
    
        public virtual ICollection<Realisation> Realisations { get; set; }
    }
}
