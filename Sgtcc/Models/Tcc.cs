//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sgtcc.Models
{
    using System;
    using System.Collections.Generic;

    public partial class Tcc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tcc()
        {
            this.Arquivoes = new HashSet<Arquivo>();
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "T�tulo")]
        [DataType(DataType.Text)]
        public string titulo { get; set; }
        [Display(Name = "Semestre")]
        [DataType(DataType.Text)]
        public string semestre { get; set; }
        [Display(Name = "Ano")]
        [DataType(DataType.Text)]
        public string ano { get; set; }
        [Display(Name = "Orientador")]
        [DataType(DataType.Text)]
        public string Orientador { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Situa��o")]
        public string situa��o { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Arquivo> Arquivoes { get; set; }
        public virtual Professor Professor { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
