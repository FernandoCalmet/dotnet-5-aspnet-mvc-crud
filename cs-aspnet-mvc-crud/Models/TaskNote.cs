//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace cs_aspnet_mvc_crud.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TaskNote
    {
        public int id { get; set; }
        public int task_id { get; set; }
        public int note_id { get; set; }
    
        public virtual Note note { get; set; }
        public virtual Task task { get; set; }
    }
}