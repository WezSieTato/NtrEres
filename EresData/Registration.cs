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
    
    public partial class Registration
    {
        public Registration()
        {
            this.GradeValues = new HashSet<GradeValue>();
        }
    
        public int RegistrationID { get; set; }
        public int StudentID { get; set; }
        public int RealisationID { get; set; }
        public string Value { get; set; }
        public byte[] TimeStamp { get; set; }
    
        public virtual ICollection<GradeValue> GradeValues { get; set; }
        public virtual Realisation Realisation { get; set; }
        public virtual Student Student { get; set; }
    }
}
