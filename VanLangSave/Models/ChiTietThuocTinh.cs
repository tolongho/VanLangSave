//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VanLangSave.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietThuocTinh
    {
        public int MaThuocTinh { get; set; }
        public int MaChiTiet { get; set; }
        public int MaSanPham { get; set; }
        public string ChiTietThuocTinh1 { get; set; }
    
        public virtual ThuocTinh ThuocTinh { get; set; }
    }
}
