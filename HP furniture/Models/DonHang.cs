//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HP_furniture.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonHang()
        {
            this.ChiTietDonHang = new HashSet<ChiTietDonHang>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> NgayDatHang { get; set; }
        public Nullable<double> TongTien { get; set; }
        public Nullable<int> ID_KhachHang { get; set; }
        public Nullable<int> ID_TrangThai { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietDonHang> ChiTietDonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual TrangThai TrangThai { get; set; }
    }
}
