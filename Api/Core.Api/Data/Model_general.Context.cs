﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Api.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities_general : DbContext
    {
        public Entities_general()
            : base("name=Entities_general")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ct_centro_costo> ct_centro_costo { get; set; }
        public virtual DbSet<ct_centro_costo_sub_centro_costo> ct_centro_costo_sub_centro_costo { get; set; }
        public virtual DbSet<in_Producto> in_Producto { get; set; }
        public virtual DbSet<in_UnidadMedida> in_UnidadMedida { get; set; }
        public virtual DbSet<tb_bodega> tb_bodega { get; set; }
        public virtual DbSet<tb_empresa> tb_empresa { get; set; }
        public virtual DbSet<tb_sucursal> tb_sucursal { get; set; }
        public virtual DbSet<tbl_bodega> tbl_bodega { get; set; }
        public virtual DbSet<tbl_producto> tbl_producto { get; set; }
        public virtual DbSet<tbl_subcentro> tbl_subcentro { get; set; }
        public virtual DbSet<tbl_usuario> tbl_usuario { get; set; }
        public virtual DbSet<tbl_usuario_x_bodega> tbl_usuario_x_bodega { get; set; }
        public virtual DbSet<tbl_usuario_x_subcentro> tbl_usuario_x_subcentro { get; set; }
        public virtual DbSet<vw_oc_x_aprobar> vw_oc_x_aprobar { get; set; }
        public virtual DbSet<vw_stock> vw_stock { get; set; }
    }
}
